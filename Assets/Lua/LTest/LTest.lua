--region *.lua
--Date
--此文件由[BabeLua]插件自动生成

function TestLua()
local tmpObj=UnityEngine.GameObject.Find("Cube")
local myTest=tmpObj:AddComponent(typeof(RoateSelf))
myTest:TestLua()
end

TestLua()
--endregion
