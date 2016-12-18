--[[
ui基类cls_ui_base
1. 维护ui全局id，记录所有打开的ui
2. 接受分发ui的特殊事件：awake,start..

]]

local UpdateBeat = UpdateBeat

cls_ui_base = class("cls_ui_base")
cls_ui_base._id = 0

-- ui管理，接受c# ui事件
local ui_manager = {}
function HandleUIMessage(id,func_name,...)
    local ui = ui_manager[id]
    if ui and ui[func_name] then
        ui[func_name](ui,...)
    end
end

function ui_manager.AddUI(ui)
    ui_manager[ui.m_unique_id] = ui
end

function ui_manager.DelUI(ui)
    ui_manager[ui.m_unique_id] = nil
end

local ui_layers = {}
function AddUIToLayer(transform,layer_order)
    layer_order = layer_order or 1
    local layer = ui_layers[layer_order]
    if not layer then
        layer = panelMgr:CreateLayer(layer_order)
        ui_layers[layer_order] = layer
    end
    transform:SetParent(layer,false)
end

function cls_ui_base:ctor()
    assert(self.s_ui_panel ~= nil,"ui必须要有对应的资源")

    -- 记录打开的ui
    cls_ui_base._id = cls_ui_base._id + 1
    self.m_unique_id = cls_ui_base._id
    ui_manager.AddUI(self)

    local obj = panelMgr:CreatePanelSync(self.m_unique_id,self.s_ui_panel)
    if obj then
        self:OnCreate(obj)
    else
        logError("cls_ui_base load "..self.s_ui_panel .. " fail")
    end
end


function cls_ui_base:EnableUpdate()
    self._update_enable = true
    UpdateBeat:Add(self.Update, self)
    -- body
end

function cls_ui_base:Update()
    -- nothing
end

function cls_ui_base:GetUniqueId()
    return self.m_unique_id
end

-- 创建panel返回调用，子类一般不需要处理这个事件
function cls_ui_base:OnCreate(obj)
    self.m_game_object = obj
    self.m_transform = obj.transform
    self.m_lua_behaviour = obj:GetComponent('LuaBehaviour');
    -- AddUIToLayer(self.m_game_object.transform, self.s_ui_order or 1)
end

function cls_ui_base:OnAwake()
    -- 这个事件用不了了
end

function cls_ui_base:OnDestroy()
    if self._update_enable then
        UpdateBeat:Remove(self.Update, self)
    end

    --移除该ui所有注册的事件
    removeNotifys(self)

    ui_manager.DelUI(self)
    log("cls_ui_base OnDestroy unique_id "..self.m_unique_id)
end

function cls_ui_base:Close()
    destroy(self.m_game_object);
end

