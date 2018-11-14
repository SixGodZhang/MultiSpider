# GitHubToMail

## 项目初衷
- 拜读Jeffrey 的大作CLR via C#,练手项目
- 真正想去熟悉C#这门语言,包括它的辐射范围,比如说数据库、网络编程等

## 项目描述
  首先，这是一个非常简单的C#项目,只是设计到的知识点比较多.比如C#基础,EF,Sqlserver,网络编程,云服务器配置等
  其次，这个项目的预想效果是 作为一个C#爬虫的集合框架,自由嵌入其他任何爬虫
  当前,项目达到的效果是 可以爬取Github趋势项目、Github关注的领域的项目、知乎的热点，并且数据可以经过清洗，存入数据库
  未来,肯定是想做数据分析的，不然采集了这些数据干嘛?比如说分析下知乎热点的走向,或者分析下用户的关系网，可以以图形化的方式展现出来;具体效果参考天眼查上的组织关系图，自行脑补

- 可以爬取Github上每日特定条件下的趋势  https://github.com/trending
- 可以根据关键字爬取github上特定主题的项目 https://github.com/search
- 可以爬取知乎热点 https://www.zhihu.com/hot
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
### Log日志模块
详细记录了程序运行中关键节点的Log信息
### 数据库模块
每一次采集的信息都存放到了数据库，方便以后的图形化分析

### 注意
- 腾讯云服务器需解封25号端口
- QQ Mail不支持HTML中内嵌CSS

### 项目图片预览
![工具截主体功能流程图](https://github.com/SixGodZhang/GitHubToMail/blob/master/images/sp2.png)
![Log模块](https://github.com/SixGodZhang/GitHubToMail/blob/master/images/sp1.png)
![项目源码一览图](https://github.com/SixGodZhang/GitHubToMail/blob/master/images/sp3.png)
![邮件截图](https://github.com/SixGodZhang/GitHubToMail/blob/master/images/11.png)


## 环境配置
移动libs目录下的所有文件到bin\Debug目录下

按格式修改配置文件github_setting.ini 和 zhihuer_setting.ini即可:
``` json

github_setting.ini
{
  "sender": "XXXXXXXX@qq.com", //发件人,qq邮箱
  "receivers": [ //接受者s
    {
	    "id": 1,//接收者id,保留
	    "name":"zhangsan",//接收者名字,保留
      "mail": "XXXXXXX@qq.com",//接受者邮箱
      "follows": ["c#"],//需要关注的每日趋势的语言
	    "themes": []//需要关注的主题
    }
  ],
  "license": "XXXXXXXXXXXX",//发件人qq邮箱的授权码
  "noticetime": "18-38",//提醒当天的时间
  "debug": false,//是否是debug模式
  "mailcontenttype":"HTML",//邮件内容格式是否采用HTML格式,可以是HTML,text
  "noticerate":"daily",//邮件发送的频率,daily、week、month、year
  "database":true//数据库存储是否开启
}

zhihuer_setting.ini
{
  "sender": "XXXXXXXX@qq.com", //发件人,qq邮箱
  "receivers": [ //接受者s
    {
	    "id": 1,//接收者id,保留
	    "name":"zhangsan",//接收者名字,保留
      "mail": "XXXXXXX@qq.com",//接受者邮箱
      "topics": ["c#"],
	    "hot":true //是否关注热点
    }
  ],
  "license": "XXXXXXXXXXXX",//发件人qq邮箱的授权码
  "noticetime": "18-38",//提醒当天的时间
  "debug": false,//是否是debug模式
  "mailcontenttype":"HTML",//邮件内容格式是否采用HTML格式,可以是HTML,text
  "noticerate":"daily",//邮件发送的频率,daily、week、month、year
  "database":true//数据库存储是否开启
}

```

