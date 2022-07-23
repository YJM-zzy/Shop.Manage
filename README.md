# 一、前台
## 1. 商品模块
+ 商品列表
+ 商品详情
+ 商品推荐
+ 商品分类
+ 商品收藏
+ 加入购物车
## 2. 订单模块
+ 创建订单
+ 订单列表
+ 订单详情
+ 取消订单
## 3. 购物车模块
+ 购物车列表
+ 修改数量
+ 删除购物车商品
## 4. 用户模块
+ 用户登录
+ 用户注册
+ 用户信息
# 二、后台
## 1. 商品模块
+ 物料上下架（物料的增删改查）
+ 物料分类管理（物料一共有三级分类）
+ 品牌管理
## 2. 订单管理
+ 订单列表
+ 修改订单状态（订单状态：1.待支付 2. 已支付 3.发货 4.收货成功（待评价） 5.评价完成）
## 3. 系统配置



# 三、数据库设计
## 1.用户模块
+ 商家表（Business)
  基本信息表(BusinessMessage): Id(主键), Name(商家名称，唯一的)，Password(密码), Role(角色 0-超管 1-普通商家), Phone(电话), CreditCode(统一信用码) avart(头像) 
+ 用户表 (User)
  基本信息表(UserMessage): Id(主键), Name(商家名称), Password(密码), Phone(电话,唯一的), avart(头像)
  地址管理表(UserAddr): Id(主键)，Country(国家/地区)， Province(省), City(市), District(区), Town(镇), Detail(详细地址)（1 对 多）
+ 头像表 (Avart): Id(主键)， ImageMessage(图片二进制数据)
+ 物料表(Material)：Id(主键)、MatNo(物料编号)、Name(物料名称)、ClassId(分类ID，外键)、Spec(规格型号)、单位(unit)、BusinessId(商家ID 外键)、PriceId(价格Id 外键)、Status（物料状态1-上架 2-下架）、Img(图片地址)、BrandId(品牌 外键)
+ 物料分类表(MaterialClass):Id(主键)、ClassNo(分类编号 一级分类：A100X,二级分类: A100X00Y,三级分类：A100X00Y00Z)、Name(分类名称)、ClassId(分类级别)
+ 价格表：Id(主键)、MatNo(物料编号)、BusinessId(商家ID)、Price（价格）
+ 品牌表：Id(主键)、BrandNo(品牌编号)、Name(名称)
