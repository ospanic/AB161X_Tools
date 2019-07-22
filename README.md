# AB161X_Tools
## 项目介绍
络达AB161X 蓝牙芯片烧录工具

## 使用方法

	AB161X_Tools com3 write_flash test.bin
	AB161X_Tools com3 read_flash 00001000 1
	
	
AT+NAME	查询或设置蓝牙广播名称	重启后生效
AT+MAC	设置或查询模组MAC地址	重启后生效
AT+NETKEY	设置Net Work Key	重启后生效
AT+APPKEY	设置Application Key	重启后生效
AT+ADDR	设置或查询设备地址	重启后生效
AT+STATE	查询设备状态	
AT+SEND	发送Mesh数据	
+MESHDATA	接收到MESH数据	
