$(document).ready(function () {
	const userTypeSelect = $('#userType');
	const dynamicFields = $('#dynamic-fields');

	function loadDynamicFields() {
		const userType = userTypeSelect.val();
		dynamicFields.empty();

		if (userType === 'Customer') {
			dynamicFields.append(`
						<div class="form-group mb-3">
							<label for="FirstName">First Name</label>
							<input name="Input.FirstName" id="FirstName" class="form-control" required />
							<span class="text-danger" data-valmsg-for="Input.FirstName" data-valmsg-replace="true"></span>
						</div>
						<div class="form-group mb-3">
							<label for="LastName">Last Name</label>
							<input name="Input.LastName" id="LastName" class="form-control" required />
							    <span class="text-danger" data-valmsg-for="Input.LastName" data-valmsg-replace="true"></span>
						</div>
									`);
			$('#FirstName').rules('add', {
				minlength: 2,
				maxlength: 20,
				messages: {
					minlength: "First name must be at least 2 characters long.",
					maxlength: "First name cannot be more than 20 characters long."
				}
			});
			$('#LastName').rules('add', {
				minlength: 2,
				maxlength: 20,
				messages: {
					minlength: "Last name must be at least 2 characters long.",
					maxlength: "Last name cannot be more than 20 characters long."
				}
			});

		} else if (userType === 'Distributor') {
			dynamicFields.append(`
												<div class="form-group mb-3">
                    <label for="CompanyName">Company Name</label>
                    <input name="Input.CompanyName" id="CompanyName" class="form-control" required/>
                    <span class="text-danger" data-valmsg-for="Input.CompanyName" data-valmsg-replace="true"></span>
                </div>
		<div class="form-group mb-3">
			<label for="TaxNumber">Tax Number</label>
			<input name="Input.TaxNumber" id="TaxNumber" class="form-control" required />
			 <span class="text-danger" data-valmsg-for="Input.TaxNumber" data-valmsg-replace="true"></span>
               
		</div>
		<div class="form-group mb-3">
			<label for="MOL">MOL</label>
			<input name="Input.MOL" id="MOL" class="form-control" required />
			<span class="text-danger" data-valmsg-for="Input.MOL" data-valmsg-replace="true"></span>
		</div>
		<div class="form-group mb-3">
			<label for="CompanyEmail">Company Email</label>
			<input name="Input.CompanyEmail" id="CompanyEmail" type="email" class="form-control" required/>
			<span class="text-danger" data-valmsg-for="Input.CompanyEmail" data-valmsg-replace="true"></span>
		</div>
		<div class="form-group mb-3">
			<label for="CompanyPhoneNumber">Company Phone Number</label>
			<input name="Input.CompanyPhoneNumber" id="CompanyPhoneNumber" type="tel" class="form-control" required/>
			<span class="text-danger" data-valmsg-for="Input.CompanyPhoneNumber" data-valmsg-replace="true"></span>
		</div>
		<div class="form-group mb-3">
			<label for="BusinessAddress">Business Address</label>
			<input name="Input.BusinessAddress" id="BusinessAddress" class="form-control" required/>
			<span class="text-danger" data-valmsg-for="Input.BusinessAddress" data-valmsg-replace="true"></span>
		</div>
		<div class="form-group mb-3">
			<label for="LicenseExpirationDate">License Expiration Date</label>
			<input name="Input.LicenseExpirationDate" id="LicenseExpirationDate" type="date" class="form-control" required/>
			<span class="text-danger" data-valmsg-for="Input.LicenseExpirationDate" data-valmsg-replace="true"></span>
		</div>
		<div class="form-group mb-3">
			<label for="DiscountRate">Discount Rate</label>
			<input name="Input.DiscountRate" id="DiscountRate" type="number" step="0.01" min="0" max="100" class="form-control" required/>
			<span class="text-danger" data-valmsg-for="Input.DiscountRate" data-valmsg-replace="true"></span>
		</div>

									`);
			$('#CompanyName').rules('add', {
				minlength: 4,
				maxlength: 20,
				messages: {
					minlength: "Company name must be at least 4 characters long.",
					maxlength: "Company name cannot be more than 20 characters long."
				}
			});

			$('#TaxNumber').rules('add', {
				minlength: 6,
				maxlength: 15,
				messages: {
					minlength: "Tax number must be at least 6 characters long.",
					maxlength: "Tax number cannot be more than 15 characters long."
				}
			});

			$('#MOL').rules('add', {
				minlength: 4,
				maxlength: 40,
				messages: {
					minlength: "MOL must be at least 4 characters long.",
					maxlength: "MOL cannot be more than 40 characters long."
				}
			});

			$('#CompanyEmail').rules('add', {
				email: true,
				minlength: 6,
				maxlength: 25,
				messages: {
					email: "Please enter a valid email address.",
					minlength: "Email must be at least 6 characters long.",
					maxlength: "Email cannot be more than 25 characters long."
				}
			});

			$('#CompanyPhoneNumber').rules('add', {
				minlength: 4,
				maxlength: 15,
				messages: {
					minlength: "Phone number must be at least 4 characters long.",
					maxlength: "Phone number cannot be more than 15 characters long."
				}
			});

			$('#BusinessAddress').rules('add', {
				minlength: 4,
				maxlength: 20,
				messages: {
					minlength: "Business address must be at least 4 characters long.",
					maxlength: "Business address cannot be more than 20 characters long."
				}
			});

			$('#LicenseExpirationDate').rules('add', {
				date: true,
				messages: {
					date: "Please enter a valid date."
				}
			});

			$('#DiscountRate').rules('add', {
				range: [0, 75],
				messages: {
					range: "Discount rate must be between 0 and 75."
				}
			});
		} else if (userType === 'Supplier') {
			dynamicFields.append(`
												<div class="form-group mb-3">
			<label for="CompanyName">Company Name</label>
			<input name="Input.CompanyName" id="CompanyName" class="form-control" required />
			<span class="text-danger" data-valmsg-for="Input.CompanyName" data-valmsg-replace="true"></span>
		</div>
		<div class="form-group mb-3">
			<label for="FactoryLocation">Factory Location</label>
			<input name="Input.FactoryLocation" id="FactoryLocation" class="form-control" required/>
			<span class="text-danger" data-valmsg-for="Input.FactoryLocation" data-valmsg-replace="true"></span>
		</div>
		<div class="form-group mb-3">
			<label for="PreferredDeliveryMethod">Preferred Delivery Method</label>
			<input name="Input.PreferredDeliveryMethod" id="PreferredDeliveryMethod" class="form-control" required/>
			<span class="text-danger" data-valmsg-for="Input.PreferredDeliveryMethod" data-valmsg-replace="true"></span>
		</div>

									`);
			$('#CompanyName').rules('add', {
				minlength: 4,
				maxlength: 20,
				messages: {
					minlength: "Company name must be at least 4 characters long.",
					maxlength: "Company name cannot be more than 20 characters long."
				}
			});
			$('#FactoryLocation').rules('add', {
				minlength: 4,
				maxlength: 20,
				messages: {
					minlength: "Factory location must be at least 4 characters long.",
					maxlength: "Factory location cannot be more than 20 characters long."
				}
			});

			$('#PreferredDeliveryMethod').rules('add', {
				minlength: 3,
				maxlength: 15,
				messages: {
					minlength: "Preferred delivery method must be at least 3 characters long.",
					maxlength: "Preferred delivery method cannot be more than 15 characters long."
				}
			});
		} else if (userType === 'WarehouseWorker') {
			dynamicFields.append(`
												<div class="form-group mb-3">
			<label for="FirstName">First Name</label>
			<input name="Input.FirstName" id="FirstName" class="form-control" required />
			<span class="text-danger" data-valmsg-for="Input.FirstName" data-valmsg-replace="true"></span>
		</div>
		<div class="form-group mb-3">
			<label for="LastName">Last Name</label>
			<input name="Input.LastName" id="LastName" class="form-control" required />
			<span class="text-danger" data-valmsg-for="Input.LastName" data-valmsg-replace="true"></span>
		</div>
		<div class="form-group mb-3">
			<label for="StartWork">Start Work</label>
			<input name="Input.StartWork" id="StartWork" type="date" class="form-control" required/>
			<span class="text-danger" data-valmsg-for="Input.StartWork" data-valmsg-replace="true"></span>
		</div>
		<div class="form-group mb-3">
			<label for="EndWork">End Work</label>
			<input name="Input.EndWork" id="EndWork" type="date" class="form-control" />
			<span class="text-danger" data-valmsg-for="Input.EndWork" data-valmsg-replace="true"></span>
		</div>

									`);
			$('#FirstName').rules('add', {
				minlength: 2,
				maxlength: 20,
				messages: {
					minlength: "First name must be at least 2 characters long.",
					maxlength: "First name cannot be more than 20 characters long."
				}
			});

			$('#LastName').rules('add', {
				minlength: 2,
				maxlength: 20,
				messages: {
					minlength: "Last name must be at least 2 characters long.",
					maxlength: "Last name cannot be more than 20 characters long."
				}
			});

			$('#StartWork').rules('add', {
				date: true,
				messages: {
					date: "Please enter a valid start date."
				}
			});

			$('#EndWork').rules('add', {
				date: true,
				messages: {
					date: "Please enter a valid end date."
				}
			});
		}
		$.validator.unobtrusive.parse(dynamicFields);
		
	}

	userTypeSelect.change(loadDynamicFields);

	loadDynamicFields();
});