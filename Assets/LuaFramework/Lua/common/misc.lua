---------------- class 定义 -----------------

function class(class_name,base_class)
    local cls = {__name = class_name}
    cls.__index = cls

    function cls:new(...)
        local obj = setmetatable({}, cls)
        cls.ctor(obj,...)
        return obj
    end

    if base_class then
        cls.super = base_class
        setmetatable(cls,{__index = base_class})
    end
    return cls
end