#!/bin/sh

echo "startup script is running"
cd /app/https
ls
cp /app/https/localhost.crt /usr/local/share/ca-certificates
update-ca-certificates
dotnet MSA.IdentityServer.dll
