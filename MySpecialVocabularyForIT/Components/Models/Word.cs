using System.ComponentModel.DataAnnotations;

namespace MySpecialVocabularyForIT.Components.Models
{
	public class Word
	{
		[Key]
		public int word_id { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 2)]
		public string word_en { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 2)]
		public string word_rus { get; set; }

		public string word_use_case { get; set; }

		public byte[]? photo { get; set; }
	}
}
