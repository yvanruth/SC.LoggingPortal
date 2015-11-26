# SC.LoggingPortal
Custom logging portal for Sitecore based on MongoDB as a central collection point with SOLR to search through the log files.

Installation:
- Build
- Deploy SC.LoggingPortal.Service to a fresh IIS site
- Retrieve SC.LoggingPortal.LogAppender.dll and place it in the bin folder of the website you want to keep track off
- Add the definition and the implementation for a new sectionGroup

``` xml
<configuration>    
    <sectionGroup name="applicationSettings" type="System.Configuration.ApplicationSettingsGroup, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089">
      <section name="SC.LoggingPortal.LogAppender.Properties.Settings" type="System.Configuration.ClientSettingsSection, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" requirePermission="false"/>
    </sectionGroup>
    ...
</configuration>
```

- Add the configSection to the web.config
``` xml
<configuration>
    <applicationSettings>
        <SC.LoggingPortal.LogAppender.Properties.Settings>
            <setting name="SC_LoggingPortal_LogAppender_SC_LoggingPortal_LogAppender_Service_SCLogger" serializeAs="String">
                <value>http://{location-of-SC.LoggingPortal.Service}/SCLogger.svc</value>
            </setting>
        </SC.LoggingPortal.LogAppender.Properties.Settings>
    </applicationSettings>
</configuration>
```

- Add the SC.LoggingPortal.LogAppender.config file to the Include folder (Sitecore 8+)
- Or add the appender to the web.config (Sitecore 6 - 7):

``` xml
<configuration>
    <log4net>
        <appender name="SCLoggingPortalAppender" type="SC.LoggingPortal.LogAppender.Log4NetMongoAppender, SC.LoggingPortal.LogAppender">
            <file value="$(dataFolder)/logs/SC.LoggingPortal.{date}.txt" />
            <layout type="log4net.Layout.PatternLayout">
                <param name="ConversionPattern" value="%d{ABSOLUTE} %-5p %c{1}:%L - %m%n" />
            </layout>
            <encoding value="utf-8" />
        </appender>
        <root>
            <priority value="INFO" />
            <appender-ref ref="SCLoggingPortalAppender" />
        </root>

        ... Default Sitecore appenders
        <appender name="LogFileAppender">
    </log4net>
</configuration>
```