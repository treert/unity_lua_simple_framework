module("battle_manager",package.seeall)

_best_score = 0
_curr_score = 0

function StartGame()
    LuaHelper.StartGame()
    _curr_score = 0
end

function ResetGame()
    LuaHelper.ResetGame()
end

function OnGameOver()
    _curr_score = LuaHelper.GetScore()
    _best_score = math.max(_curr_score,_best_score)
end

function GetScoreData()
    return _curr_score,_best_score
end