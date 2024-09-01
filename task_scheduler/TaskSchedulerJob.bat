@echo off
set current_dir=%~dp0
set script_name=TaskSchedulerJobScript.sql
set sqlscript_path=%current_dir%%script_name%
set server_name=DESKTOP-J46BGO7\SQLEXPRESS
set db_name=LoggingDB
set username=loggingdb_admin
set pwd=passworD123
sqlcmd -U %username% -S %server_name% -d %db_name% -P %pwd% -i %sqlscript_path%
pause
cls
exit