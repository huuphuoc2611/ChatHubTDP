using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web;
using System.Globalization;
using System.Web.Mvc;
using ChatHubTDP.Models;

namespace ChatHubTDP.Controllers
{
    public class KhachhangController : Controller
    {
        // GET: Khachhang
        DBChatHub db = new DBChatHub();
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult ThemSanPham()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                ViewBag.MaCD = new SelectList(db.ChuDes.OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            return View();

        }

        [System.Web.Mvc.HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemPhuKien(SanPham sanPham, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                //kiem tra đường dẫn file
                if (fileUpload == null)
                {
                    ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                //thêm vào csdl
                else
                {
                    if (ModelState.IsValid)
                    {
                        // lưu tên file, lưu ý bổ sung thư viện System.IO;
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        // lưu đường dẫn ccủa file
                        var path = Path.Combine(Server.MapPath("~/images"), fileName);
                        // kiểm tra hình ảnh tồn tại chưaaaaaaa?
                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            // lưu hình ảnh vào đường dẫn
                            fileUpload.SaveAs(path);
                        }
                        sanPham.Anhbia = fileName;
                        // lưu vào csdl
                        db.SanPhams.InsertOnSubmit(sanPham);
                        db.SubmitChanges();
                    }
                    return RedirectToAction("PhuKien", "Admin");
                }


            }
        }
        // sửa sản phẩm
        public ActionResult SuaPhuKien(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {

                // var phukien = from s in db.PHUKIENs where s.MaPK == id select s;
                PHUKIEN phukien = db.PHUKIENs.SingleOrDefault(n => n.MaPK == id);
                // lấy DL từ table chude để đổ vào dropdownlist kèm theo chọn MaCD tương ứng
                ViewBag.MaCD = new SelectList(db.CHUDEs.OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
                return View(phukien);
            }
        }
        [HttpPost, ActionName("SuaPhuKien")]
        [ValidateInput(false)]
        public ActionResult XacNhanSua(PHUKIEN phukien, HttpPostedFileBase fileUpload)
        {

            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                PHUKIEN phukienUpdate = db.PHUKIENs.SingleOrDefault(n => n.MaPK == phukien.MaPK);

                //kiem tra đường dẫn file
                if (fileUpload != null)
                {
                    // lưu tên file, lưu ý bổ sung thư viện System.IO;
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    // lưu đường dẫn ccủa file
                    var path = Path.Combine(Server.MapPath("~/images"), fileName);
                    // kiểm tra hình ảnh tồn tại chưaaaaaaa?
                    if (!System.IO.File.Exists(path))
                    {
                        // lưu hình ảnh vào đường dẫn
                        fileUpload.SaveAs(path);
                    }

                    phukienUpdate.Anhbia = fileName;
                }

                UpdateModel(phukienUpdate);
                db.SubmitChanges();
                return RedirectToAction("PhuKien", "Admin");
            }
        }
        // chi tiết sản phẩm
        public ActionResult ChiTietPK(int id)
        {
            //lấy ra đối tượng phụ kiện theo mã
            PHUKIEN phukien = db.PHUKIENs.SingleOrDefault(n => n.MaPK == id);
            ViewBag.MaPK = phukien.MaPK; ;
            if (phukien == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(phukien);
        }

        // xoa san pham
        [HttpGet]
        public ActionResult XoaPhuKien(int id)
        {
            PHUKIEN phukiendelete = db.PHUKIENs.SingleOrDefault(n => n.MaPK == id);
            ViewBag.MaPK = phukiendelete.MaPK;
            if (phukiendelete == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(phukiendelete);
        }

        [HttpPost, ActionName("XoaPhuKien")]
        public ActionResult XacNhanXoa(int id)
        {
            // lấyy ra đối tượng cần xoá theo mã
            PHUKIEN phukiendelete = db.PHUKIENs.SingleOrDefault(n => n.MaPK == id);
            ViewBag.MaPK = phukiendelete.MaPK;
            if (phukiendelete == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.PHUKIENs.DeleteOnSubmit(phukiendelete);
            db.SubmitChanges();
            return RedirectToAction("PhuKien");
        }
        //QL chu de
        public ActionResult ListChuDe()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(db.CHUDEs.ToList());
        }
        //Them Chu de
        [HttpGet]
        public ActionResult CreateCD()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateCD(CHUDE chude)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                db.CHUDEs.InsertOnSubmit(chude);
                db.SubmitChanges();
                return RedirectToAction("ListChuDe");
            }
        }
        // Sua chu de
        public ActionResult EditCD(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var chude = from cd in db.CHUDEs where cd.MaCD == id select cd;
                return View(chude.SingleOrDefault());
            }
        }
        [System.Web.Http.HttpPost, System.Web.Http.ActionName("EditCD")]
        public ActionResult CapNhatCD(int id)
        {
            CHUDE chude = db.CHUDEs.Where(n => n.MaCD == id).SingleOrDefault();
            UpdateModel(chude);
            db.SubmitChanges();
            return RedirectToAction("ListChuDe");
        }

        // xoa chu de
        [System.Web.Http.HttpGet]
        public ActionResult DeleteCD(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var chude = from cd in db.CHUDEs where cd.MaCD == id select cd;
                return View(chude.SingleOrDefault());
            }
        }

        [HttpPost, ActionName("DeleteCD")]
        public ActionResult XacNhanXoaCD(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                CHUDE chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
                db.CHUDEs.DeleteOnSubmit(chude);
                db.SubmitChanges();
                return RedirectToAction("ListChuDe");
            }
        }
    }
}