using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using MySpecialVocabularyForIT.Data;
using MySpecialVocabularyForIT.Components.Models.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MySpecialVocabularyForIT.Components.Models
{
	public class Word : IValidatableObject
	{
		[Key]
		public int word_id { get; set; }

		[Required(ErrorMessage = "Term in English is required.")]
		[StringLength(100, MinimumLength = 2)]
		[AllowedEnglishChars("a-zA-Z0-9.,!?'\"()#+\\-<>")]
		public string word_en { get; set; }

		[Required(ErrorMessage = "Translation into Russian is required.")]
		[StringLength(500, MinimumLength = 2)]
		public string word_rus { get; set; }
		[Required(ErrorMessage = "Use explication is required.")]
		public string word_use_case { get; set; }

		public byte[]? photo { get; set; }

		public string? URL { get; set; }
		//public string? photoContentType { get; set; }
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var results = new List<ValidationResult>();

			// 1. Проверка уникальности word_en
			if (!string.IsNullOrWhiteSpace(word_en))
			{
				// Получаем DbContextFactory из ValidationContext.ServiceProvider
				var dbContextFactory = validationContext.GetService<IDbContextFactory<MySpecialVocabularyForITContext>>();
				if (dbContextFactory == null)
				{
					// Если DbContextFactory недоступен, что маловероятно в Blazor
					results.Add(new ValidationResult("Database context factory not available."));
				}
				else
				{
					using (var context = dbContextFactory.CreateDbContext())
					{
						// Проверяем, существует ли уже запись с таким word_en,
						// ИСКЛЮЧАЯ текущую редактируемую запись.
						var exists = context.Words.Any(d =>
							d.word_en.ToLower() == word_en.ToLower() &&
							d.word_id != this.word_id // Это ключевое условие для редактирования
						);

						if (exists)
						{
							// Добавляем ошибку валидации.
							// Имя поля должно соответствовать имени свойства в модели.
							results.Add(new ValidationResult($"A term with the name '{word_en}' already exists.", new[] { nameof(word_en) }));
						}
					}
				}
			}

			return results;
		}
	}
}
