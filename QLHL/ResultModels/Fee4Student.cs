using QLHL.Datas;
using QLHL.Helper;

namespace QLHL.ResultModels
{
    public class Fee4Student
    {
        public int totalFee { get; set; }
        public PageResult<Fee> result { get; set; }

        public static int CalFee(PageResult<Fee> pageResult)
        {
            int totalFee = 0;
            foreach (var item in pageResult.data)
            {
                if (item.status == "Not Yet") totalFee += item.cost;
            }
            return totalFee;
        }
    }
}
