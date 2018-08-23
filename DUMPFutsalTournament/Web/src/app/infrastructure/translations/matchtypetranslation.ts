import { MatchType } from '../enums/matchtype';

export class MatchTypeTranslation {
	 GetMatchTypeTranslation(enumValue: number): string {
		  switch (enumValue) {
			case MatchType.Group:
				return 'Grupa';
			case MatchType.QuarterFinal:
		    	     return 'Četvrtfinale';
               case MatchType.SemiFinal:
				return 'Polufinale';
		     case MatchType.ThirdPlace:
				return 'Za 3. mjesto';
			case MatchType.Final:
				return 'Finale';
			case MatchType.Revial:
				return 'Revijalna utakmica';
			default:
				return 'Nepoznato';
		 }

	 }
}