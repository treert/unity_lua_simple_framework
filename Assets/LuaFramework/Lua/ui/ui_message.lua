--[[
消息弹窗
]]

local cls_ui_message = class("cls_ui_message",cls_ui_base)
cls_ui_message.s_ui_panel = 'oms_test/PanelMessage'
cls_ui_message.s_ui_order = 100
local l_instance = nil

function cls_ui_message:ctor(msg)
    self.super.ctor(self)
    self.m_msg = msg
end

function cls_ui_message:OnStart()
    self.m_transform = self.m_game_object.transform
    self.m_btn = self.m_transform:FindChild("PanelRoot/Button").gameObject;
    self.m_transform:FindChild("PanelRoot/TextTip"):GetComponent('Text').text = tostring(self.m_msg)

    self.m_lua_behaviour:AddClick(self.m_btn, function (obj)
        self:Close()
    end);
end

function cls_ui_message:OnDestroy()
    self.super.OnDestroy(self)
    l_instance = nil
end

function ShowMessageUI(msg)
    l_instance = cls_ui_message:new(msg)
end