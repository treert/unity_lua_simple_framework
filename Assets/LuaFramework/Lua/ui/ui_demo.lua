local cls_ui_demo = class("cls_ui_demo",cls_ui_base)
cls_ui_demo.s_ui_panel = 'uiprefab/demo_ui'
cls_ui_demo.s_ui_order = 12

function cls_ui_demo:ctor()
    self.super.ctor(self)
end

function cls_ui_demo:OnStart()
    log("cls_ui_demo OnStart")
    -- UILabel组件
    self.m_count = 333
    local label_num = self.m_transform:FindChild("LabelContain/LabelNum");
    self.m_ui_label_demo = label_num:GetComponent("UILabel")
    self.m_ui_label_demo.text = self.m_count

    -- Button click事件
    self.m_is_stoped = true
    self.m_ui_button_demo = self.m_transform:FindChild("Button").gameObject;
    self.m_lua_behaviour:AddClick(self.m_ui_button_demo, function (obj)
        -- 切换开关
        self.m_is_stoped = not self.m_is_stoped
        log("change stop status to "..tostring(self.m_is_stoped))
    end);

    -- 启动每帧更新，下面的Update的执行依赖这个
    self:EnableUpdate()
end

function cls_ui_demo:Update()
    if self.m_is_stoped then
        return
    end

    -- 计数加1
    self.m_count = self.m_count - 1
    self.m_ui_label_demo.text = self.m_count

    if self.m_count <= 0 then
        self:Close()
        log("count down to zero ")
    end
end

function cls_ui_demo:OnDestroy()
    self.super.OnDestroy(self)
    log("cls_ui_demo OnDestroy")
end


function ShowDemoUI()
    cls_ui_demo:new()
end