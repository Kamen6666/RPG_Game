--输出日志--
function log(str)
   Util.Log(str);
end

--打印字符串--
function print(str) 
	Util.Log(str);
end
--蓝色打印
function printBlue(str) 
	Util.Log("<color=blue>"..str.."</color>");
end
--绿色打印
function printGreen(str) 
	Util.Log("<color=green>"..str.."</color>");
end
--红色打印
function printRed(str) 
	Util.Log("<color=red>"..str.."</color>");
end
--错误日志--
function error(str) 
	Util.LogError(str);
end

--警告日志--
function warn(str) 
	Util.LogWarning(str);
end

--查找对象--
function find(str)
	return GameObject.Find(str);
end

function destroy(obj)
	GameObject.Destroy(obj);
end
function destroyWait(obj,tim)
	GameObject.Destroy(obj,tim);
end
function newobject(prefab)
	return GameObject.Instantiate(prefab);
end
