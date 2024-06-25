using System;

namespace EA.Tekton.TechnicalTest.WebApi.Requests;

public class ProductRequest
{
	        public int ProductId { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }

}