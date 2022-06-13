# AppServiceHandsOn

## OverView

このリポジトリは Azure App Service ハンズオンで使用する プロジェクトが格納してあります。

- EasuAuth
- ExtraStorage
- FileTransferToStorage
- FileUploaderToLocalDisk
- GitHubDeploy
- LocalGitDeploy
- RunFromPacakge
- Scaling
- ZipUploadUI
- ZipUploadUI-SrcDeploy

## EasuAuth

EasyAuth が設定されている環境では、ログインユーザーのユーザー名とメールアドレスが表示されるようになっています。
設定がない場合は表示されません。

## ExraStorage

/mounts/files という Path で Azur Files をマウントする前提のWebアプリケーションです。
ローカルで起動する時は C:\mounts\files ディレクトリを参照します。

Pathに存在するファイルを表示するだけのWebアプリケーションです。

## FileTransferToStorage
FileUploaderToLocalDisk の Web アプリケーションによって AppService の内部ストレージにアップロードされたファイルに対して、Queue に格納されたファイル名のファイルを Azure Storage にアップロードし、その後内部ストレージのファイルを削除します。アプリケーション設定として以下のセットアップをします。

| 名前                  | 値                        |
| --------------------- | ------------------------- |
| AzureWebJobsDashboard | Azure Storageの接続文字列 |
| AzureWebJobsStorage   | Azure Storageの接続文字列 |
| blobContainerName     | uploadfiles               |
| QueueName             | uploadfilenames           |
| uploadFileDestDirName | uploadfiles               |

## FileUploaderToLocalDisk

ファイルをアップロードして AppServiceが既定で持つ内部ストレージにファイルを格納し、ファイル名を Azure Queue Storage に格納します。そのため Azure Storage が必須です。
アプリケーション設定として以下のセットアップをします。
| 名前                  | 値              |
| --------------------- | --------------- |
| QueueName             | uploadfilenames |
| uploadFileDestDirName | uploadfiles     |

## GitHubDeploy

index.cshtml に GitHub 経由で Deploy したことを示す文字列が記載してあるだけです。

## Scaling

スケールアウトのテストを行うためのに負荷を受け付けるWebアプリケーションです。アクセスされるとインスタンス名を Cosmos Db に保存する仕様のため、Cosmos Db の作成が必須です。App Service に本プロジェクトをデプロイ後に次の２つの設定をアプリケーション設定に追加してください。
| 名前             | 値                           |
| ---------------- | ---------------------------- |
| CosmosDb:Account | <Cosmo sDb の URI>           |
| CosmosDb:Key     | <Cosmos Db のプライマリキー> |

あとは画面を触れば使い方は理解できるかと思います。負荷を掛ける時には Azure Load Testing を使うと簡単です。負荷をかけるURLは

> https://<AppService名>.azurewebsites.net/applyload

です。負荷ですが、AppServcie の SKU が S1 の場合は同時アクセス 500 スレッドもあれば十分です。メトリックスには Requests 数を使うとわかりやすくスケールアウトするでしょう。

## LocalGitDeploy

index.cshtml に LocalGit で Deploy したことを示す文字列が記載してあるだけです。

## RunFromPackage

index.cshtml に RunFromPacakge で Deploy したことを示す文字列が記載してあるだけです。

## ZipUploadUI

index.cshtml に ZipUploadUI で Deploy したことを示す文字列が記載してあるだけです。

## ZipUploadUI-SrcDeploy

index.cshtml に ZipUploadUI で ソースから Deploy したことを示す文字列が記載してあるだけです。
