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

按格式修改配置文件setting.ini即可:
``` json
{
  "sender": "XXXXXXXX@qq.com", //发件人,qq邮箱
  "receivers": [ //接受者s
    {
	  "id": 1,//接收者id,保留
	  "name":"zhangsan",//接收者名字,保留
      "mail": "XXXXXXX@qq.com",//接受者邮箱
      "follow": ["c#"],//需要关注的每日趋势的语言
	  "themes": []//需要关注的主题
    }
  ],
  "license": "XXXXXXXXXXXX",//发件人qq邮箱的授权码
  "noticetime": "18-38",//提醒当天的时间
  "debug": false,//是否是debug模式
  "mailcontenttype":"HTML",//邮件内容格式是否采用HTML格式,可以是HTML,text
  "noticerate":"daily",//邮件发送的频率,daily、week、month、year
}
```

## 工具截图:
![工具截图](https://github.com/SixGodZhang/GitHubToMail/blob/master/images/11.png)



