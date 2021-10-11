# 登录接口

- [ ] 前端已完成
- [x] 后端已完成

- **接口说明**: 用于账号登录
- **请求地址**: `login`
- **请求方式**: `POST`

请求参数 | 参数名 | 参数类型 | 是否必填 | 说明
-|-|-|:-:|:-
用户ID|id|string|是|-
用户密码|password|string|是|加密方式暂未确定

返回值名| 返回值 | 说明
:-|:-:|:-
code|返回码|可能值：1001,4001
msg|信息|登录成功/用户不存在/密码错误/内部错误，请联系管理员
data|数据|登录成功：token、role、name.若登录失败则为null
&emsp;token|token|
&emsp;role|角色类型|
&emsp;name|用户姓名|

## 返回值示例

```json
{
  "code":1001,
  "msg":"login successfully",
  "data":{
    "token":"ad544704-ae65-44f7-ba21-49b82fb77cca",
    "role":"1",
    "name":"myName"
  }
}
```
