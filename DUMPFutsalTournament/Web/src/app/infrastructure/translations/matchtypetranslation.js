"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var matchtype_1 = require("../enums/matchtype");
var MatchTypeTranslation = /** @class */ (function () {
    function MatchTypeTranslation() {
    }
    MatchTypeTranslation.prototype.GetMatchTypeTranslation = function (enumValue) {
        switch (enumValue) {
            case matchtype_1.MatchType.Group:
                return 'Grupa';
            case matchtype_1.MatchType.QuarterFinal:
                return 'Četvrtfinale';
            case matchtype_1.MatchType.SemiFinal:
                return 'Polufinale';
            case matchtype_1.MatchType.ThirdPlace:
                return 'Za 3. mjesto';
            case matchtype_1.MatchType.Final:
                return 'Finale';
            case matchtype_1.MatchType.Revial:
                return 'Revijalna utakmica';
            default:
                return 'Nepoznato';
        }
    };
    return MatchTypeTranslation;
}());
exports.MatchTypeTranslation = MatchTypeTranslation;
//# sourceMappingURL=matchtypetranslation.js.map