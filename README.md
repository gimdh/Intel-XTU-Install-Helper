# Intel XTU Installation Helper

Intel has been dropped XTU support for certain CPUs (Such as 8550u, 8250u, and etc), resulting installer to reject installation process with **'unsupported platform_error'**. However, in most cases, this does not mean your hardware is actually incompatible with XTU. 

Therefore, XTU can still be installed with [simple trick by Kalle Lilja](https://kallelilja.com/2018/12/workaround-xtu-attempted-to-install-on-an-unsupported-platform/), and used for CPU tweaks, such as undervolting.

Intel XTU Install Helper is designed to automate such procedure, providing an easier way to install XTU on unsupported platforms.


# User Guide

 1. Download Intel XTU Installer from [official intel download center](https://downloadcenter.intel.com/download/24075/Intel-Extreme-Tuning-Utility-Intel-XTU-).
 2. Download Intel XTU Install Helper from  [repository release tab](https://github.com/gimdh/Intel-XTU-Install-Helper/releases).
 3. Run Intel XTU Install Helper
	 - You can execute with an argument in first place.
	 > cmd> **IntelXTU_Install_Helper.exe <path/to/Original XTU Installer from Intel>**
	 - Or can enter a path after execution.
	 > \> Please enter the file name of proper XTU Installer: **<path/to/Original XTU Installer form Intel>**


If any error occurs, please submit a new issue with created **error.log** file.


## Example

**Assume**
- Both files are located in the same directory.
- File name of **Intel XTU Installer from Intel** is **XTUSetup.exe**
- File name of **Intel XTU Install Helper from this repository** is **IntelXTU_Install_Helper.exe**

**Then**
- Run **IntelXTU_Install_Helper.exe XTUSetup.exe** in cmd (or PowrShell).
	**or**
- Open **IntelXTU_Install_Helper.exe** by double-click.
- Program should prompt **Please enter the file name of proper XTU Installer:**
- Type **XTUSetup.exe** then press enter.

Now follow the directions of the program.


# Comment
It was origianlly my private 30-minute project. However, I think it's quite handy, so added least amount of user-friendliness in order to share this nifty tool.

So please bare with my code if you happen to go through it. Thanks.




