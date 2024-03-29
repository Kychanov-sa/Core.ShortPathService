# Описание сервиса коротких ссылок ShortPathService

## Конфигурация сервиса
Конфигурация сервиса настраивается через файл appsettings.json.
Она содержит параметры общего назначения, а также разделена на отдельные секции.

### Строки соединения с базой данных и сервисами
В секции **ConnectionStrings** содержатся строки подключения для соединения с базой данных сервиса:
- **Database** - задает строку подключения к базе данных сервиса.

### Настройки логирования
В секции **Logging** содержатся параметры, которые управляют возможностями логирования сервиса.

### Общие настройки
В секции **General** содержатся параметры, которые управляют общими настройками сервиса:
- **ServiceEndpoint** - задает адрес конечной точки сервиса. Должен совпадать с адресом, по которому доступен сервис;
- **DataCenterId** - задаёт идентификатор центра обработки банных, в котором размещён экземпляр сервиса;
- **ServiceInstanceId** - задаёт идентификатор экземпляра сервиса в ЦОД.

### Настройки диагностики
В секции **Diagnostics** содержатся параметры, которые управляют диагностическими возможностями сервиса:
 - блок **Profiling** - содержит настройки профайлинга;
 - блок **Logging** - содержит настройки журналирования;
 - блок **Tracing** - содержит настройки трассировки;
 - параметр **EnableConfigLogging** - признак задает включение записи в лог данных из конфигурации, при этом секретные данные (пароли, ключи и т.п.) логироваться не будут.

 #### Настройка профайлинга
 Настройка задаётся в блоке Profiling секции Diagnostics и включает в себя следующие параметры:
 - параметр **Enabled** - признак задает включение логирования запросов и ответов для исходящих из сервиса запросов.

 #### Настройка журналирования
 Настройка задаётся в блоке Logging секции Diagnostics и включает в себя следующие параметры:
 - параметр **Enabled** - признак задает включение журналирования работы сервиса.

 #### Настройка трассировки
 Настройка задаётся в блоке Tracing секции Diagnostics и включает в себя следующие параметры:
 - параметр **Enabled** - признак задает включение трассировки запросов сервиса;
 - параметр **ServiceName** - задает имя сервиса, под которым он будет обозначен в запросах трассировки;
 - параметр **AgentHost** - задает адрес агента, собирающего данные трассировки;
 - параметр **AgentPort** - задает порт агента, собирающего данные трассировки.
 
### Настройки безопасности
В секции **Security** содержатся параметры, которые управляют настройками безопасности сервиса:
- **EnableAudit** - признак задает включение аудита входящих запросов.

### Настройки глобализации
В секции **Globalization** содержатся параметры, которые управляют настройками для глобализации:
- **DefaultCulture** - задает культуру для локализации по умолчанию.

### Настройка аутентификации
В сервисе используется аутентификация пользователей с использованием JWT токенов, выданных сторонними доверенными центрами идентификации.
Для настройки аутентификации используется секция Authentication, которая включает в себя следующие настройки:
- блок **TrustedIssuers** - содержит список настроек доверенных издателей токенов;
- параметр **Audience** - задаёт аудиторию сервиса, которая используется для проверки целевого назначения 

#### Настройка доверенных издателей токенов
В блоке **TrustedIssuers** секции Authentication, задаётся список групп настроек, по одной для каждого издателя.
В каждой группе настроек издателя задаются следующие параметры:
- **Issuer** - имя издателя токена. Значение должно быть заполнено;
Пример: "Issuer": "ContosoIdentity"
- **EncryptionKey** - ключ, которым издатель шифрует токен. Значение может быть пустым;
Пример: "EncryptionKey": "wbtQtYEihuj6Kf89PiVEwZ5GvUKVvQte"
- **SigningCertificateThumbprint** - отпечаток сертификата, которым издатель подписывает токен. Сам сертификат должен быть помещен в личное хранилище локальной машины. Значение может быть пустым;
Пример: "SigningCertificateThumbprint": "BC46C4B6E87BB840D6C7C162DC1646D9B7537AC9"
- **SigningCertificatePath** - путь до файла с сертификатом, которым издатель подписывает токен. Значение может быть пустым;
Пример: "SigningCertificatePath": "С:\\Certificates\\IdentityService.crt"
- **SigningCertificatePfxPassword** - задает пароль от pfx сертификата для подписания токенов. Используется если в **SigningCertificatePath** указан pfx сертификат. Значение может быть пустым;
Пример: "SigningCertificatePfxPassword": "12345"


Настройки **SigningCertificateThumbprint** и **SigningCertificatePath** являются взаимоисключающими. В случае, если заданы оба значения, более приоритетным будет **SigningCertificatePath**.