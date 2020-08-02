# ESAPI_Beam-Namer
(Binary-Plugin)
Simply change your Beam/Field IDs with the nomenclature of your choice.

First-Compile tips:
- add your own ESAPI-DLL-Files (VMS.TPS.Common.Model.API.dll + VMS.TPS.Common.Model.Types). Usually found in C:\Program Files\Varian\RTM\15.1\esapi\API
- For clinical Mode: Approve the produced .dll in External Treatment Planning if 'Is Writeable = true'

Note:
- script is optimized to work with Eclipse 15.1
- absolute beginner should first read my beginnerGuide
https://drive.google.com/drive/folders/1-aYUOIfyvAUKtBg9TgEETiz4SYPonDOO

Good Learning Task:
- read João Castelos Blog-Post regarding the same topic: https://medium.com/@jhmcastelo/writing-in-the-eclipse-database-change-the-fields-id-based-on-gantry-angle-312462f8fc67
- add the BeamIdChanger.cs file to my project and utilize it. this will enable more flexibility for naming your beams. here is a screenshot from the blog post which is showing the naming scheme of 'BeamIdChanger.cs'
![Test Image 1](https://github.com/Kiragroh/ESAPI_Beam-Namer/blob/master/pics/BeamNamingScheme_CopiedFromJoaoCastelo.png)
