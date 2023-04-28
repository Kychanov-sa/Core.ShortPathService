# Сервис коротких ссылок ShortPathService

Чтобы установить и подготовить к работе сервис коротких ссылок:

1. [Создайте и инициализируйте базу данных](##Создание и инициализация базы данных сервиса коротких ссылок).
1. [Установите сервис коротких ссылок](##Установка сервиса коротких ссылок).
1. [Настройте сервис коротких ссылок](##Настройка сервиса коротких ссылок).
1. [Проверьте корректность настройки](##Проверка корректности настройки сервиса коротких ссылок).

Для установки сервиса требуются:

* SQL Server Management Studio и Microsoft SQL Server либо PgAdmin и PostgreSQL для размещения базы данных, в которой сервис хранит информацию о маршрутах;
* сертификат, которым сервис идентификации подписывает выпускаемые JWT-токены, и который будет использоваться сервисом коротких ссылок для проверки действительности токенов.

## Создание и инициализация базы данных сервиса коротких ссылок

1. С помощью SQL Server Management Studio или PgAdmin создайте базу данных ShortPathes.
1. Для инициализации схемы базы данных выполните скрипт InitializeDatabase.sql. Скрипт расположен в папке дистрибутива ShortPathService в подпапке DatabaseScripts:
   * для Microsoft SQL Server – DatabaseScripts/SqlServer/InitializeDatabase.sql;       
   * для PostgreSQL – DatabaseScripts/PostgreSql/InitializeDatabase.sql.

## Установка сервиса коротких ссылок

### Установка сервиса коротких ссылок на сервере с Windows

1. В папке C:\inetpub\wwwroot создайте подпапку CoreShortPathService.
1. Из папки дистрибутива с ZIP-пакетами компоненты ShortPathService в созданную папку извлеките содержимое архива WebApi.Service-win-x64.   
1. В диспетчере служб IIS добавьте новый сайт CoreShortPathService, указав **Физический путь** до папки CoreShortPathService. Пример:  
   `C:\inetpub\wwwroot\CoreShortPathService`
1. В настройках пула приложений для созданного сайта укажите параметры:
   * **Версия среды CLR.NET** – «Без управляемого кода»;
   * **Режим управляемого контейнера** – «Встроенный».
1. Задайте реквизиты учетной записи для запуска пула приложений. У учетной записи должны быть полные права на папку с сервисом.
1. В настройках сайта в разделе «Проверка подлинности» включите анонимную проверку подлинности.
1. Установите значение переменной окружения ASPNETCORE_ENVIRONMENT в соответствии с назначением сервиса:
   * **Development** - для рабочего окружения разработчика;
   * **Staging** - для препродуктивного стенда;
   * **Production** - для продуктивного стенда;
1. В окне настройки привязок укажите:
   * для протокола HTTP – порт **80**;
   * для протокола HTTPS – порт **443** и доверенный SSL-сертификат.


### Установка сервиса коротких ссылок на сервере с Linux

Для разворачивания сервиса на платформе Linux используется docker. Соберите docker-образ сервиса, либо используйте готовый.

1. Создайте папку /opt/configs/shortpathservice, в которой будут располагаться конфигурационные файлы сервиса.
1. Скопируйте сертификат открытого ключа проверки токенов сервиса идентификации в файл /opt/configs/shortpathservice/certificates/ids-jwt.crt.
1. Создайте файл docker-compose.yml. Подробнее см. раздел [«Пример настроек сервиса документов в конфигурационном файле docker-compose.yml»](####Пример настроек сервиса документов в конфигурационном файле docker-compose.yml).
1. В файле docker-compose.yml укажите заранее выбранный внешний порт сервиса в docker-compose.yml. В [примере](####Пример настроек сервиса коротких ссылок в конфигурационном файле docker-compose.yml) используется порт **44320**.


#### Пример настроек сервиса коротких ссылок в конфигурационном файле docker-compose.yml

```yaml
version: '3.8'

services:
  shortpath-service:
    image: registry.directum.ru/hrpro/shortpathservice:1.0.4
    entrypoint: "sh -c 'cp /certificates/*.crt /usr/local/share/ca-certificates/ && update-ca-certificates && dotnet GlacialBytes.Core.ShortPathService.WebApi.Service.dll'"
    environment:
      ASPNETCORE_URLS: "https://+:443"
      ASPNETCORE_Kestrel__Certificates__Default__Path: "/certificates/shortpath-service-ssl.pfx"
      ASPNETCORE_Kestrel__Certificates__Default__Password: ""

      Authentication__SigningCertificatePath: "/certificates/ids-jwt.crt"

      ConnectionStrings__Database: "ProviderName=System.Data.SqlClient;Data source=;Initial catalog=;User ID=;Password="
    volumes:
      - ./certificates:/certificates:ro
    ports:
      - "44320:443" 
```

## Настройка сервиса коротких ссылок

### Настройка сервиса коротких ссылок на сервере с Windows

В папке с сервисом C:\inetpub\wwwroot\CoreShortPathService в конфигурационном файле appsettings.json заполните параметры:

* в блоке **ConnectionStrings**:
  * в строке подключения **Database** укажите параметры для соединения с базой данных маршрутов.  
    Если используется база данных Microsoft SQL Server, то в строке подключения в параметре **ProviderName** укажите значение **System.Data.SqlClient**.  
    Если используется база данных PostgreSQL, то в строке подключения в параметре **ProviderName** укажите значение **Npgsql**.  
    Если значение не указано, то по умолчанию подключение осуществляется к базе данных Microsoft SQL Server.

* в блоке **General**:
  * в параметре **ServiceEndpoint** укажите внешний адрес сервиса, по которому он будет доступен пользователям;
  * в параметре **DataCenterId** задайте идентификатор ЦОД, в котором размещён сервис;
  * в параметре **ServiceInstanceId** задайте идентификатор экземпляра сервиса в ЦОД;
* в секции **Authentication** добавьте новый блок настроек в параметр TrustedIssuers, заполните следующие значения:
  * **Issuer** укажите имя издателя JWT токенов;
  * **SigningCertificateThumbprint** укажите отпечаток сертификата, которым подписываются токены сервисом идентификации.
 
### Настройка сервиса коротких ссылок на сервере с Linux

В конфигурационном файле docker-compose.yml заполните параметры:

* **image** – ссылка на Docker-образ с указанием версии;
* **Authentication__TrustedIssuers__0__SigningCertificatePath** – файл открытого ключа сертификата для проверки токенов;
* **Authentication__TrustedIssuers__0__Issuer** - укажите имя издателя JWT токенов;
* **ConnectionStrings__Database** – строка подключения к базе данных маршрутов;
* **General__ServiceEndpoint** - укажите внешний адрес сервиса, по которому он будет доступен пользователям;
* **General__DataCenterId** - задайте идентификатор ЦОД, в котором размещён сервис;
* **General__ServiceInstanceId** - задайте идентификатор экземпляра сервиса в ЦОД.

#### Запуск сервиса коротких ссылок

Запустите сервис коротких ссылок. Для этого:
1. Авторизуйтесь в репозитории registry.directum.ru:
   > docker login registry.directum.ru
1. Перейдите в папку с docker-compose.yml и выполните команду:
   > docker stack deploy -c .\docker-compose.yml shortpath-service --with-registry-auth

Если сервис необходимо удалить, выполните команду:
> docker stack rm shortpath-service

## Проверка корректности настройки сервиса коротких ссылок

Чтобы проверить настройки:

1. Перезапустите сервис, чтобы применить заданные параметры.
1. В браузере откройте страницу:
   > https://<адрес сервиса коротких ссылок>/ready
1. Если настройка корректна, будет получен ответ:

```json
{
  "Entries": {
    ...
  "Status": "Healthy",
  "TotalDuration": "00:00:00.9590891"
}
```

