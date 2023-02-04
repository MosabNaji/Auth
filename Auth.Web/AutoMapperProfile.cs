using Auth.Web.Models;
using Auth.Web.ViewModel;
using AutoMapper;

namespace Auth.Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<User, UserViewModel>().ForMember(x => x.UserEmail , x => x.MapFrom(x =>x.Email)); //نوع البيانات يعني لأي كيان تنتمي و ليس شكلها كقائمة أو قاموس , اليوزر ايميل تاعت اليوزر فيو مودل بتاخد قيمتها من الايميل تاع اليوزر
            CreateMap<User , UserViewModel>().ForMember(x => x.UserName , x => x.Ignore()); //تجاهل لليوزر نيم, الأوتو مابر عشان يشتغل لازم يكونوا كل الخواص نفس النوع و نفس الاسم , و بستخدم دالة التجاهل لما يكون عندي خاصيتين مش نفس النوع و بدي أحولهم لبعض فبعمللهم تجاهل من الأوتو مابر زي الصورة نوعها اي فورم فايل و بدي احولها لخاصية من نوع نص سترينق
            CreateMap<CreateUserViewModel, User>(); // من كرييت يوزر فيو مودل ليوزر 
        }
    }
}
