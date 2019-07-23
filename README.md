# AB161X_Tools
## 项目介绍

络达AB161X 蓝牙芯片烧录工具，可使用串口对AB1611等芯片进行烧录

### 接线方式

|USB To TTL | AB161X |
|-----------|--------|
|  Vcc      |  Vcc   |
|  Gnd      |  Gnd   |
|  Tx       |  Rx    |
|  Rx       |  Tx    |
|  RTS      |  Rst   |
|  DTR      |  OD_IO1|


工具分为桌面版和控制台版两种形式，下面分别介绍

## AB1611 Tool Form 
![image](https://raw.githubusercontent.com/ospanic/AB161X_Tools/master/MainForm.png)

如上图，选择要烧录的固件，设置串口后，点击Start即可

## AB1611 Tool Consale

	AB1611_Tools.exe  comXX read_flash <addr> <len> <out_file_name>
	AB1611_Tools.exe  comXX write_flash  <file_name>
	
	
