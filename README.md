# AppServiceHandsOn

## OverView

このリポジトリは Azure App Service ハンズオンで使用する プロジェクトが格納してあります。

- EasuAuth
- ExtraStorage
- GitHubDeploy
- LocalGitDeploy
- RunFromPacakge
- ZipUploadUI
- ZipUploadUI-SrcDeploy

## EasuAuth

EasyAuth が設定されている環境では、ログインユーザーのユーザー名とメールアドレスが表示されるようになっています。
設定がない場合は表示されません。

## ExraStorage
/mounts/files という Path で Azur Files をマウントする前提のWebアプリケーションです。
ローカルで起動する時は C:\mounts\files ディレクトリを参照します。

Pathに存在するファイルを表示するだけのWebアプリケーションです。

## GitHubDeploy
index.cshtml に GitHub 経由で Deploy したことを示す文字列が記載してあるだけです。

## LocalGitDeploy
index.cshtml に LocalGit で Deploy したことを示す文字列が記載してあるだけです。

## RunFromPackage
index.cshtml に RunFromPacakge で Deploy したことを示す文字列が記載してあるだけです。

## ZipUploadUI
index.cshtml に ZipUploadUI で Deploy したことを示す文字列が記載してあるだけです。

## ZipUploadUI-SrcDeploy
index.cshtml に ZipUploadUI で ソースから Deploy したことを示す文字列が記載してあるだけです。
