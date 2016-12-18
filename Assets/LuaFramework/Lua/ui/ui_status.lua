local cls_ui_status = class("cls_ui_status",cls_ui_base)
cls_ui_status.s_ui_panel = 'uiprefab/StatusUI'
local l_instance = nil
require "logic/battle_manager"

function cls_ui_status:ctor()
    self.super.ctor(self)
end

function cls_ui_status:OnStart()
    local label_score = self.m_transform:FindChild("LabelScore");
    self.m_UILabel_score = label_score:GetComponent("UILabel")
    registerGlobalEvent("ENUM_GAME_OVER", self, function ()
        self:Close()
    end)

    self:EnableUpdate()
end

function cls_ui_status:Update( ... )
    local score = LuaHelper.GetScore()
    self.m_UILabel_score.text = score
end

function cls_ui_status:OnDestroy()
    self.super.OnDestroy(self)
    l_instance = nil
end


function ShowStatusUI()
    battle_manager.OnGameOver()
    l_instance = cls_ui_status:new()
end