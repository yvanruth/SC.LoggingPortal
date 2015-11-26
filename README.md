# SC.LoggingPortal
Custom logging portal for Sitecore based on MongoDB as a central collection point with SOLR to search through the log files.

Installation:
- Build
- Deploy SC.LoggingPortal.Service to a fresh IIS site
- Retrieve SC.LoggingPortal.LogAppender.dll and place it in the bin folder of the website you want to keep track off
- Add the appender to the web.config:

``` xml
<configuration>
    <log4net>
        <appender name="SCLoggingPortalAppender" type="SC.LoggingPortal.LogAppender.Log4NetMongoAppender, SC.LoggingPortal.LogAppender">
            <file value="$(dataFolder)/logs/SC.LoggingPortal.{date}.txt" />
            <layout type="log4net.Layout.PatternLayout">
                <conversionPattern value="%d{ABSOLUTE} %-5p %c{1}:%L - %m%n" />
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