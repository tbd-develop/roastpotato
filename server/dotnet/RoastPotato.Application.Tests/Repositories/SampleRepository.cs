using System.Collections.Generic;

namespace RoastPotato.Application.Tests.Repositories
{
    public class SampleRepository
    {
        public IEnumerable<SampleData> DataSource { get; set; } 

        public IEnumerable<SampleData> GetSampleData( )
        {
            IEnumerable<SampleData> data = new[ ]
                                               {
                                                   new SampleData {Address = "Test 1"},
                                                   new SampleData {Address = "Test 2"}
                                               };

            return data;
        }
    }
}