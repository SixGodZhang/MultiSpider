# GitHubToMail

## 项目描述
- 可以爬取Github上每日特定条件下的趋势  https://github.com/trending
- 可以根据关键字爬取github上特定主题的项目 https://github.com/search
- 将爬取的结果以邮件的方式发送至订阅者邮箱

## 功能描述
### 爬取Github趋势
因为Github没有提供获取趋势的API,所以此功能采用的是解析HTML文档.
### 爬取Github话题
根据Github提供的接口，发出请求获取json字符串，然后对其进行解析
### 邮箱内容
提供两种格式,一种是普通文本，一中是HTML格式.
### 定时发送邮件
可以在每天、每周、每月、每年的固定时间点发送邮件.此部分保留了特殊日期，便于扩展
### 注意
- 腾讯云服务器需解封25号端口
- QQ Mail不支持HTML中内嵌CSS


## 环境配置
移动libs目录下的所有文件到bin\Debug目录下




