﻿using WeCantSpell.Hunspell;

namespace TagCloudGenerator
{
    public class BoringWordsTextProcessor : TextProcessor
    {
        private readonly ITextProcessor textProcessor;
        public BoringWordsTextProcessor(ITextProcessor textProcessor) { this.textProcessor = textProcessor; }

        public override IEnumerable<string> ProcessText(IEnumerable<string> text)
        {
            text = textProcessor.ProcessText(text);

            var wordList = WordList.CreateFromFiles(
                "../../../Dictionaries/English (American).dic",
                "../../../Dictionaries/English (American).aff");
            
            foreach (var word in text)
            {               
                var details = wordList.CheckDetails(word);
                var wordEntryDetails = wordList[string.IsNullOrEmpty(details.Root) ? word : details.Root];
              
                if (wordEntryDetails.Length != 0 && wordEntryDetails[0].Morphs.Count != 0)
                {
                    var po = wordEntryDetails[0].Morphs[0];

                    if (po == "po:pronoun" || po == "po:preposition" 
                        || po == "po:determiner" || po == "po:conjunction")
                        continue;
                }
                
                yield return word;
            }
        }
    }
}