<?xml version="1.0" encoding="utf-8"?>
<!-- 本配置文件的文件名称为 xxx.dll.config-->
<!--   在 web.config文件中添加全局设置 
<appSettings>
    <add key="EnableCaching" value="false" />
    <add key="CacheDuration" value="60" />
    <add key="PageSize" value="10" />
</appSettings>
-->
<configuration>
  <connectionStrings>
	  <!--请不要更改默认连接名称-->
	  <add name="defaultConnection" connectionString="$constr$" providerName="System.Data.SqlClient" />

	  <!--
    <add name="defaultConnection" connectionString="Data Source=.;Initial Catalog=$database$;uid=$uid$;pwd=$pwd$;" providerName="System.Data.SqlClient" />
    -->
	  <!--Integrated Security=True-->
    <!--   <add name="defaultConnection" connectionString="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\DocTran.mdb"
        providerName="System.Data.OleDb" />-->
  </connectionStrings>
  
  <DataProvider>
  $meta$
	<!--
    /*begin
    <add interface="I$table$Provider" providerType="$namespace$.DAL.SqlClient.Sql$table$Provider,$assembly$,Version=1.0.0.0"/>  
	end*/
    -->
  </DataProvider>

</configuration>
