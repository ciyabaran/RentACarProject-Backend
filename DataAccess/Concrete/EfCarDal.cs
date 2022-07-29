using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete
{
    public class EfCarDal : EfEntityFrameworkBase<Car, MsSqlContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (MsSqlContext context = new MsSqlContext())
            {
                var result =
                    from c in context.Cars
                    join b in context.Brands
                        on c.BrandId equals b.Id
                    join clr in context.Colors
                        on c.ColorId equals clr.Id

                    select new CarDetailDto
                    {
                        CarName = c.CarName,
                        BrandName = b.Name,
                        ColorName = clr.Name,
                        DailyPrice = c.DailyPrice
                    };
                return result.ToList();
            }
        }
    }
}
