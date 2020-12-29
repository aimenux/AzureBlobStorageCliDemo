![.NET](https://github.com/aimenux/AzureBlobStorageCliDemo/workflows/.NET/badge.svg)
# AzureBlobStorageCliDemo
```
Providing net core global tool for uploading/downloading files from/to blob storage
```

> In this demo, i m building a global tool that allows to upload or download blobs from or to azure storage. 
The tool use [shellprogressbar](https://github.com/Mpdreamz/shellprogressbar) library in order to display the progress percentage of each operation.
>
> The tool is based on one main command and two sub commmands :
>> - Use sub command `UploadFileCli` to upload file to blob storage
>> - Use sub command `DownloadFileCli` to download file from blob storage
>
> In order to run the demo, type the following commands in your favorite terminal : 
>> - `.\App.exe UploadFileCli -cs ConnectionString -cn ContainerName -bn BlobName -fp FilePath`
>> - `.\App.exe DownloadFileCli -cs ConnectionString -cn ContainerName -bn BlobName -fp FilePath`
>
> To install, run, uninstall global tool from a local source path, type commands :
>> - ` dotnet tool install -g --add-source ./app/cli-tools azureblob-cli --configfile .\nuget.config`
>> - `azureblob-cli -h`
>> - `azureblob-cli UploadFileCli -h`
>> - `azureblob-cli DownloadFileCli -h`
>> - `dotnet tool uninstall azureblob-cli -g`

**`Tools`** : vs19, net core 3.1