"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var MatchEvent = /** @class */ (function () {
    function MatchEvent(match, player, eventType, isForHomeTeam, eventMinute) {
        this.match = match;
        this.player = player;
        this.eventType = eventType;
        this.isForHomeTeam = isForHomeTeam;
        this.eventMinute = eventMinute;
    }
    return MatchEvent;
}());
exports.MatchEvent = MatchEvent;
//# sourceMappingURL=matchevent.js.map