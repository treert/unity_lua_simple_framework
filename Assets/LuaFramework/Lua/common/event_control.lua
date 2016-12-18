--[[


使用方法
1. 监听某个数据
data = {}

function cls_ui_:OnDataChange()
end
registerEvent(data, "ENUM_DATA_CHANGE", self, self.OnDataChange)

triggerEvent(data, "ENUM_DATA_CHANGE", data)


2. 监听全局消息
function OnSomeThingHappend()
end

registerGlobalEvent("ENUM_SOME_HAPPEND", nil, OnSomeThingHappend)

triggerGlobalEvent("ENUM_SOME_HAPPEND","params")

]]


local l_events = {}
local l_delayRemoveNotifys = {}

local l_globalObj = "global"

--注册事件
--[triggerObj][triggerEvent][notifyObj]
function registerEvent(triggerObj, triggerEvent, notifyObj, callbackFunc)

    if triggerObj==nil or triggerEvent==nil or callbackFunc==nil then
        return
    end
    notifyObj = notifyObj or callbackFunc
    local t1 = l_events[triggerObj]
    if t1==nil then
        t1 = {}
        l_events[triggerObj] = t1
    end
    local t2 = t1[triggerEvent]
    if t2==nil then
        t2 = {}
        t1[triggerEvent] = t2
    end
    local t3 = t2[notifyObj]
    if t3==nil then
        t3 = {}
        t2[notifyObj] = t3
    end

    t3.m_callback = callbackFunc
    t3.m_bDeleteMe = false

end

-- 注册全局事件
function registerGlobalEvent(triggerEvent, notifyObj, callbackFunc)
    registerEvent(l_globalObj, triggerEvent, notifyObj, callbackFunc)
end

-- 删除事件
local function __realRemoveNotifys()

    local function tableHasElement(tab)
        for k, v in pairs(tab) do
            return true
        end
        return false
    end

    if l_delayRemoveNotifys~=nil then
        local pairs = pairs
        for _,rObj in pairs(l_delayRemoveNotifys) do
            if rObj~=nil then
                for k1,t1 in pairs(l_events) do
                    for k2,t2 in pairs(t1) do
                        local t3 = t2[rObj]
                        if t3~=nil and t3.m_bDeleteMe then
                            t2[rObj] = nil
                        end -- end if

                        if not tableHasElement(t2) then
                            t1[k2] = nil
                        end -- end if
                    end --end for

                    if not tableHasElement(t1) then
                        l_events[k1] =nil
                    end
                end -- end for
            end -- end if
        end -- end for
        l_delayRemoveNotifys = nil
    end
end

-- 触发事件
local function __triggerEvent(triggerObj, triggerEvent, ...)
    if triggerObj==nil or triggerEvent==nil then
        return
    end

    local t1 = l_events[triggerObj]
    if t1==nil then
        return
    end

    local t2 = t1[triggerEvent]
    if t2==nil then
        return
    end

    local pairs = pairs
    for notifyObj, t3 in pairs(t2) do
        if notifyObj~=nil and (not t3.m_bDeleteMe) then
            t3.m_callback( notifyObj, triggerObj, triggerEvent, ... )
        end
    end
end

function triggerEvent(triggerObj, triggerEvent, ... )
    __triggerEvent(triggerObj, triggerEvent, ...)

    __realRemoveNotifys()
end

function triggerGlobalEvent(triggerEvent, ...)
    __triggerEvent(l_globalObj, triggerEvent, ...)

    __realRemoveNotifys()
end

--移除通知事件
function removeNotifys(notifyObj)
    if notifyObj==nil then
        return
    end

    l_delayRemoveNotifys = l_delayRemoveNotifys or {}
    l_delayRemoveNotifys[notifyObj] = notifyObj

    local pairs = pairs
    for _,t1 in pairs(l_events) do
        for _,t2 in pairs(t1) do
            local t3 = t2[notifyObj]
            if t3~=nil then
                t3.m_bDeleteMe = true
            end
        end
    end
end

--移除特定事件
function removeSpecialNotify(triggerObj, triggerEvent, notifyObj)
    if notifyObj==nil then
        return
    end

    l_delayRemoveNotifys = l_delayRemoveNotifys or {}
    l_delayRemoveNotifys[notifyObj] = notifyObj

    local t1 = l_events[triggerObj]
    if t1~=nil then
        local t2 = t1[triggerEvent]
        if t2~=nil then
            local t3 = t2[notifyObj]
            if t3~=nil then
                t3.m_bDeleteMe = true
            end
        end
    end
end

function removeGlobalNotify(triggerEvent, notifyObj)
    removeSpecialNotify(l_globalObj, triggerEvent, notifyObj)
end