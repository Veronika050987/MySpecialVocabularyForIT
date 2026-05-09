using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MySpecialVocabularyForIT.Components.Models.Validators
{
	public class AllowedEnglishCharsAttribute : ValidationAttribute
	{
		// Определяем разрешенные символы.
		// ^ - начало строки
		// [a-zA-Z0-9.,!?'"()\- ] - разрешенные символы:
		//   a-z : строчные английские буквы
		//   A-Z : прописные английские буквы
		//   0-9 : цифры
		//   .,!?'"()\- : знаки препинания и пробел (например)
		//   + - один или более символов из предыдущего набора
		// $ - конец строки
		private const string AllowedCharsPattern = @"^[a-zA-Z0-9.,!?'""()\-<> ]+$";
		private readonly string _allowedCharsDescription;

		public AllowedEnglishCharsAttribute(string allowedChars)
		{
			// Сохраняем строку разрешенных символов для сообщения об ошибке
			_allowedCharsDescription = allowedChars;
			// Задаем сообщение об ошибке по умолчанию, которое будет включать описание разрешенных символов
			ErrorMessage = $"The field {{0}} must contain only the following characters: {_allowedCharsDescription}.";
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null || string.IsNullOrWhiteSpace((string)value))
			{
				// Если поле пустое, оно может быть необязательным (если нет [Required])
				// Если оно обязательное, [Required] атрибут позаботится об этом.
				return ValidationResult.Success;
			}

			var stringValue = value.ToString();

			if (Regex.IsMatch(stringValue!, AllowedCharsPattern))
			{
				return ValidationResult.Success; // Ввод соответствует шаблону
			}
			else
			{
				// Создаем сообщение об ошибке, если есть другие атрибуты ([Required], [StringLength])
				// Если ErrorMessage уже задан в конструкторе, можно использовать его.
				// Если нужно адаптировать его динамически, здесь можно добавить форматирование.
				// В данном случае, мы будем использовать ErrorMessage, который задали в конструкторе.
				return new ValidationResult(string.Format(ErrorMessageString, validationContext.DisplayName));
			}
		}
	}
}
