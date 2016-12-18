local cls_ui_start = class("cls_ui_start",cls_ui_base)
cls_ui_start.s_ui_panel = 'uiprefab/BeginUI'
local l_instance = nil
require "logic/battle_manager"

function cls_ui_start:ctor()
    self.super.ctor(self)
end

function cls_ui_start:OnStart()
    self.m_btn_start = self.m_transform:FindChild("BtnBegin").gameObject;

    self.m_lua_behaviour:AddClick(self.m_btn_start, function (obj)
        battle_manager.StartGame()
        require "ui/ui_status"
        ShowStatusUI()
        self:Close()
    end);
end

function cls_ui_start:OnDestroy()
    self.super.OnDestroy(self)
    l_instance = nil
end


function ShowStartUI()
    l_instance = cls_ui_start:new()
end