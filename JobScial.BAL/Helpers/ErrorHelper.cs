﻿using BMOS.BAL.Errors;
using FluentValidation.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMOS.BAL.Helpers
{
    public static class ErrorHelper
    {
        public static string GetErrorString(string errorStr)
        {
            List<ErrorDetail> errors = new List<ErrorDetail>();

            ErrorDetail errorDetail = new ErrorDetail()
            {
                FieldNameError = "Exception",
                DescriptionError = new List<string> { errorStr }
            };

            errors.Add(errorDetail);

            return JsonConvert.SerializeObject(errors);
        }
        
        public static string GetErrorString(string fieldName, string errorStr)
        {
            List<ErrorDetail> errors = new List<ErrorDetail>();

            ErrorDetail errorDetail = new ErrorDetail()
            {
                FieldNameError = fieldName,
                DescriptionError = new List<string> { errorStr }
            };

            errors.Add(errorDetail);

            return JsonConvert.SerializeObject(errors);
        }

        public static string GetErrorString(List<string> errorStr)
        {
            List<ErrorDetail> errors = new List<ErrorDetail>();

            ErrorDetail errorDetail = new ErrorDetail()
            {
                FieldNameError = "Exception",
                DescriptionError = errorStr
            };

            errors.Add(errorDetail);

            return JsonConvert.SerializeObject(errors);
        }

        public static string GetErrorsString(ValidationResult validationResult)
        {
            List<ErrorDetail> errors = new List<ErrorDetail>();
            foreach (var error in validationResult.Errors)
            {
                ErrorDetail errorDetail = errors.FirstOrDefault(x => x.FieldNameError.Equals(error.PropertyName));
                if (errorDetail == null)
                {
                    List<string> descriptionError = new List<string>();
                    descriptionError.Add(error.ErrorMessage);
                    ErrorDetail newErrorDetail = new ErrorDetail()
                    {
                        FieldNameError = error.PropertyName,
                        DescriptionError = descriptionError
                    };

                    errors.Add(newErrorDetail);
                }
                else
                {
                    errorDetail.DescriptionError.Add(error.ErrorMessage);
                }
            }

            var message = JsonConvert.SerializeObject(errors);
            return message;
        }
    }
}
