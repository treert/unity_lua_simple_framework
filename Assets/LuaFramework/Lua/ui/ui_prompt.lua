--[[
测试列表，有个按钮
]]

local cls_ui_prompt = class("cls_ui_prompt",cls_ui_base)
cls_ui_prompt.s_ui_panel = 'oms_test/PromptPanel'
local l_instance = nil

function cls_ui_prompt:ctor()
    self.super.ctor(self)
end

function cls_ui_prompt:OnStart()
    self.m_transform = self.m_game_object.transform
    self.m_btnOpen = self.m_transform:FindChild("Open").gameObject;
    self.m_gridParent = self.m_transform:FindChild('ScrollView/Grid');

    self.m_lua_behaviour:AddClick(self.m_btnOpen, function (obj)
        -- self:Close()
        require "ui/ui_message"
        ShowMessageUI()
    end);

    self:InitPanel()
end

--初始化面板--
function cls_ui_prompt:InitPanel()
    local obj = panelMgr:GetGameObject('oms_test/PromptItem')
    if not obj then
        logError("fail create item")
        return
    end
    local count = 100;
    local parent = self.m_gridParent;
    for i = 1, count do
        local go = newObject(obj);
        go.name = 'Item'..tostring(i);
        go.transform:SetParent(parent);
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        self.m_lua_behaviour:AddClick(go,function (go)
            log(go.name);
        end)

        local label = go.transform:FindChild('Text');
        label:GetComponent('Text').text = tostring(i);
    end
end

function cls_ui_prompt:OnDestroy()
    self.super.OnDestroy(self)
    l_instance = nil
end


function ShowPromptUI()
    l_instance = cls_ui_prompt:new()
end