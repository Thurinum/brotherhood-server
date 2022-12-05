Remove-Item -Force -Recurse .\Brotherhood_Server\Migrations
Add-Migration OwO
Drop-Database
Update-Database