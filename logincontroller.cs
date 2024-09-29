 public ActionResult Clogin()
 {
     return View();
 }

 [HttpPost]
 [ValidateAntiForgeryToken]
 public ActionResult Clogin(CustomerLogin login)
 {
     using (ElectricityBillPortalFinalCaseStudyEntities mk = new ElectricityBillPortalFinalCaseStudyEntities())
     {
         var ob = mk.Customers
             .Where(a => a.Username.Equals(login.Username) && a.Password.Equals(login.Password))
             .FirstOrDefault();

         if (ob != null)
         {
             Session["UserId"] = ob.CustomerId.ToString();
             // Login success logic here (e.g., redirect to a different page)
             return RedirectToAction("Index");
         }
         else
         {
             // Set an error message if the login fails
             ModelState.AddModelError("", "Invalid username or password.");
         }
     }

     return View(login);
 }
