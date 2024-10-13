## Setup local DB
`docker pull mcr.microsoft.com/azure-sql-edge`
`docker run -e "ACCEPT_EULA=1" -e "MSSQL_SA_PASSWORD=P@ssword123" -e "MSSQL_PID=Developer" -p 1436:1433 -d --name=BethanyPieShopDB mcr.microsoft.com/azure-sql-edge`