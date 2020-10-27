using Aquaforest.Pdfa.Validator;
using System;

namespace Aquaforest.PDF
{
	public class PDFAValidator
	{
		public PDFAValidator()
		{
		}

		public Aquaforest.PDF.PDFAValidationResult ValidatePDFA(string doc, AquaforestPDFAFlavour flavour)
		{
			Aquaforest.Pdfa.Validator.PDFAValidationResult pDFAValidationResult = AquaforestValidator.ValidatePDFA((int)flavour, doc);
			return new Aquaforest.PDF.PDFAValidationResult()
			{
				IsValid = pDFAValidationResult.IsValid,
				ValidationResult = pDFAValidationResult.ValidationResult
			};
		}
	}
}