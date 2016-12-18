local cls_ui_game_over = class("cls_ui_game_over",cls_ui_base)
cls_ui_game_over.s_ui_panel = 'uiprefab/ResultUI'
local l_instance = nil
require "logic/battle_manager"

function cls_ui_game_over:ctor()
    self.super.ctor(self)
end

function cls_ui_game_over:OnStart()
    self.m_btn_reset = self.m_transform:FindChild("BtnRetry").gameObject;
    self.m_lua_behaviour:AddClick(self.m_btn_reset, function (obj)
        battle_manager.ResetGame()
        SendUIMessage("ENUM_SHOW_START_UI")
        self:Close()
    end);

    local score,best = battle_manager.GetScoreData()

    local label_score = self.m_transform:FindChild("ObjScore/LabelScore");
    self.m_UILabel_score = label_score:GetComponent("UILabel")
    self.m_UILabel_score.text = score

    local label_score = self.m_transform:FindChild("ObjScore/LabelBest");
    self.m_UILabel_best = label_score:GetComponent("UILabel")
    self.m_UILabel_best.text = best
end

function cls_ui_game_over:OnDestroy()
    self.super.OnDestroy(self)
    l_instance = nil
end


function ShowGameOverUI()
    battle_manager.OnGameOver()
    l_instance = cls_ui_game_over:new()
end