using EVRental.MVCWebApp.QuanNH.Models;
using EVRentalWCFServiceReference;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace EVRental.MVCWebApp.QuanNH.Controllers
{
    public class CheckOutQuanNhsController : Controller
    {
        private static CheckOutQuanNhSoapServiceClient CreateClient()
        {
            return new CheckOutQuanNhSoapServiceClient(CheckOutQuanNhSoapServiceClient.EndpointConfiguration.BasicHttpBinding_ICheckOutQuanNhSoapService);
        }

        public async Task<IActionResult> Index()
        {
            var client = CreateClient();
            var viewModels = new List<CheckOutQuanNhViewModel>();

            try
            {
                var items = await client.GetAllAsync();
                if (items != null)
                {
                    viewModels = items.Select(MapToViewModel)
                                      .Where(model => model != null)
                                      .ToList();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Không thể tải danh sách check-out. {ex.Message}";
            }
            finally
            {
                CloseClient(client);
            }

            return View(viewModels);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var client = CreateClient();

            try
            {
                var item = await client.GetByIdAsync(id.Value);
                if (item == null)
                {
                    return NotFound();
                }

                return View(MapToViewModel(item));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Không thể tải thông tin chi tiết. {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
            finally
            {
                CloseClient(client);
            }
        }

        public async Task<IActionResult> Create()
        {
            await PopulateReturnConditionsAsync();

            return View(new CheckOutQuanNhViewModel
            {
                CheckOutTime = DateTime.Now
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CheckOutQuanNhViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                await PopulateReturnConditionsAsync(viewModel.ReturnConditionId);
                return View(viewModel);
            }

            var client = CreateClient();

            try
            {
                var dto = MapToDto(viewModel);
                var result = await client.CreateAsync(dto);

                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Tạo mới bản ghi thành công.";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Không thể tạo mới bản ghi.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Lỗi: {ex.Message}");
            }
            finally
            {
                CloseClient(client);
            }

            await PopulateReturnConditionsAsync(viewModel.ReturnConditionId);
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var client = CreateClient();

            try
            {
                var item = await client.GetByIdAsync(id.Value);
                if (item == null)
                {
                    return NotFound();
                }
                var viewModel = MapToViewModel(item);
                await PopulateReturnConditionsAsync(viewModel?.ReturnConditionId);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Không thể tải dữ liệu chỉnh sửa. {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
            finally
            {
                CloseClient(client);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CheckOutQuanNhViewModel viewModel)
        {
            if (id != viewModel.CheckOutQuanNhid)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                await PopulateReturnConditionsAsync(viewModel.ReturnConditionId);
                return View(viewModel);
            }

            var client = CreateClient();

            try
            {
                var dto = MapToDto(viewModel);
                var result = await client.UpdateAsync(dto);

                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Cập nhật bản ghi thành công.";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Không thể cập nhật bản ghi.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Lỗi: {ex.Message}");
            }
            finally
            {
                CloseClient(client);
            }

            await PopulateReturnConditionsAsync(viewModel.ReturnConditionId);
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }

            var client = CreateClient();

            try
            {
                var item = await client.GetByIdAsync(id.Value);
                if (item == null)
                {
                    return NotFound();
                }

                return View(MapToViewModel(item));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Không thể tải dữ liệu xoá. {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
            finally
            {
                CloseClient(client);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = CreateClient();

            try
            {
                var result = await client.DeleteAsync(id);

                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Đã xoá bản ghi thành công.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xoá bản ghi.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi xoá bản ghi: {ex.Message}";
            }
            finally
            {
                CloseClient(client);
            }

            return RedirectToAction(nameof(Index));
        }

        private static CheckOutQuanNhViewModel MapToViewModel(CheckOutQuanNh entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new CheckOutQuanNhViewModel
            {
                CheckOutQuanNhid = entity.CheckOutQuanNhid,
                CheckOutTime = entity.CheckOutTime,
                ReturnDate = entity.ReturnDate,
                ExtraCost = entity.ExtraCost,
                TotalCost = entity.TotalCost,
                LateFee = entity.LateFee,
                IsPaid = entity.IsPaid,
                IsDamageReported = entity.IsDamageReported,
                Notes = entity.Notes,
                CustomerFeedback = entity.CustomerFeedback,
                PaymentMethod = entity.PaymentMethod,
                StaffSignature = entity.StaffSignature,
                CustomerSignature = entity.CustomerSignature,
                ReturnConditionId = entity.ReturnConditionId,
                ReturnConditionName = entity.ReturnCondition?.Name
            };
        }

        private static CheckOutQuanNh MapToDto(CheckOutQuanNhViewModel viewModel)
        {
            var dto = new CheckOutQuanNh
            {
                CheckOutQuanNhid = viewModel.CheckOutQuanNhid,
                CheckOutTime = viewModel.CheckOutTime,
                ReturnDate = viewModel.ReturnDate?.Date,
                ExtraCost = viewModel.ExtraCost,
                TotalCost = viewModel.TotalCost,
                LateFee = viewModel.LateFee,
                IsPaid = viewModel.IsPaid,
                IsDamageReported = viewModel.IsDamageReported,
                Notes = viewModel.Notes,
                CustomerFeedback = viewModel.CustomerFeedback,
                PaymentMethod = viewModel.PaymentMethod,
                StaffSignature = viewModel.StaffSignature,
                CustomerSignature = viewModel.CustomerSignature,
                ReturnConditionId = viewModel.ReturnConditionId
            };

            if (viewModel.ReturnConditionId.HasValue)
            {
                dto.ReturnCondition = new ReturnCondition
                {
                    ReturnConditionId = viewModel.ReturnConditionId.Value
                };
            }

            return dto;
        }

        private async Task PopulateReturnConditionsAsync(int? selectedId = null)
        {
            var client = CreateClient();

            try
            {
                var items = await client.GetReturnConditionsAsync();
                var options = items?
                    .OrderBy(item => item.Name)
                    .Select(item => new SelectListItem
                    {
                        Value = item.ReturnConditionId.ToString(),
                        Text = string.IsNullOrWhiteSpace(item.Name) ? $"Tình trạng #{item.ReturnConditionId}" : item.Name,
                        Selected = selectedId.HasValue && item.ReturnConditionId == selectedId
                    })
                    .ToList() ?? new List<SelectListItem>();

                options.Insert(0, new SelectListItem
                {
                    Value = string.Empty,
                    Text = "-- Chọn tình trạng trả xe --",
                    Selected = !selectedId.HasValue
                });

                ViewBag.ReturnConditions = options;
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Không thể tải danh sách tình trạng trả xe. {ex.Message}";
                ViewBag.ReturnConditions = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Value = string.Empty,
                        Text = "Không thể tải dữ liệu tình trạng"
                    }
                };
            }
            finally
            {
                CloseClient(client);
            }
        }

        private static void CloseClient(CheckOutQuanNhSoapServiceClient client)
        {
            if (client == null)
            {
                return;
            }

            try
            {
                if (client.State != CommunicationState.Faulted)
                {
                    client.Close();
                }
                else
                {
                    client.Abort();
                }
            }
            catch
            {
                client.Abort();
            }
        }
    }
}
