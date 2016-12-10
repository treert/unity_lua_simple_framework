--[[
测试列表，有个按钮
]]

local cls_ui_login = class("cls_ui_login",cls_ui_base)
cls_ui_login.s_ui_panel = 'oms_test/PanelLogin'
local l_instance = nil

function cls_ui_login:ctor()
    self.super.ctor(self)
end

function cls_ui_login:OnStart()
    self.m_transform = self.m_game_object.transform
    self.m_btn_login = self.m_transform:FindChild("ButtonLogin").gameObject;
    self.m_input_name = self.m_transform:FindChild("InputFieldName/Text"):GetComponent('Text')
    self.m_input_passwd = self.m_transform:FindChild("InputFieldPasswd/Text"):GetComponent('Text')

    self.m_lua_behaviour:AddClick(self.m_btn_login, function (obj)
        local name = self.m_input_name.text
        local passwd = self.m_input_passwd.text
        -- self:Close()
        require "ui/ui_message"
        ShowMessageUI("帐号：".. name .."\n" .. "密码：".. passwd)
    end);
end

function cls_ui_login:OnDestroy()
    self.super.OnDestroy(self)
    l_instance = nil
end


function ShowLoginUI()
    l_instance = cls_ui_login:new()
end