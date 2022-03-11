class CustomTooltip {
	init(params) {
		const eGui = (this.eGui = document.createElement('div'));
		const color = params.color || 'white';
		const data = params.api.getDisplayedRowAtIndex(params.rowIndex).data;

		eGui.classList.add('custom-tooltip');
		eGui.style['background-color'] = color;
		eGui.innerHTML = `
            <p>
                ${data.description}
            </p>
        `;
	}

	getGui() {
		return this.eGui;
	}
}

const tooltipValueGetter = (params) => ({ value: params.description });
const razerviewSearchMgr = {

	dateComparator: function (date, date2) {
		var date1Number = workshopSearchMgr.monthToComparableNumber(date);
		var date2Number = workshopSearchMgr.monthToComparableNumber(date2);

		if (date1Number === null && date2Number === null) {
			return 0;
		}
		if (date1Number === null) {
			return -1;
		}
		if (date2Number === null) {
			return 1;
		}

		return date1Number - date2Number;
	},

	monthToComparableNumber: function (date) {
		if (date === undefined || date === null || date.length !== 17) {
			return null;
		}

		var yearNumber = date.substring(0, 4);
		var monthNumber = date.substring(5, 7);
		var dayNumber = date.substring(8, 10);

		var result = yearNumber * 10000 + monthNumber * 100 + dayNumber;
		return result;

	},

	configureResultsGrid: function () {

		var columnDefs = [

			// Date Column
			{
				suppressSizeToFit: true,
				headerName: "Date",
				field: "startTime",
				sortable: true,
				cellStyle: { textAlign: 'center' },
				comparator: this.dateComparator,
				width: 125,
				// necessary evil for now
				tooltipComponentParams: { color: '#ececec' },
				cellRenderer: (s) => {
					const div = document.createElement('div');
					var sspan = document.createElement('span')
					sspan.setAttribute("data-utcTime", s.data.startTime);
					sspan.setAttribute("data-utcFormat", "MM/dd/yyyy");

					$.leo.global.processSpanUTCTimes(sspan)
					div.appendChild(sspan);
					return div
				}
			},

			// Day Column
			{
				suppressSizeToFit: true,
				width: 125,
				headerName: "Day", field: "day",
				// necessary evil for now
				tooltipComponentParams: { color: '#ececec' },
			},

			// Time Column
			{
				suppressSizeToFit: true,
				width: 175,
				headerName: "Time",
				// necessary evil for now
				field: "startTime",
				tooltipComponentParams: { color: '#ececec' },
				comparator: this.dateComparator,
				cellRenderer: (s) => {
					/*
					const eDiv = document.createElement('div');
					eDiv.innerHTML = '<span class="my-css-class"><button class="btn-simple">Push Me</button></span>';
					const eButton = eDiv.querySelectorAll('.btn-simple')[0];
					eButton.addEventListener('click', () => {
						console.log('button was clicked!!');
					});

					return eDiv;
					*/
					const div = document.createElement('div');
					var sspan = document.createElement('span')
					sspan.setAttribute("data-utcTime", s.data.startTime);
					sspan.setAttribute("data-utcFormat", "hh:mm tt");
					$.leo.global.processSpanUTCTimes(sspan)
					div.appendChild(sspan);
					div.append(" - ");

					var espan = document.createElement('span')
					espan.setAttribute("data-utcTime", s.data.endTime);
					espan.setAttribute("data-utcFormat", "hh:mm tt");
					$.leo.global.processSpanUTCTimes(espan)
					div.appendChild(espan);

					return div

				},
			},
			// Title Column
			{
				flex: 3,
				headerName: "Workshop Title", field: "workshopTitle",
				// necessary evil for now
				tooltipField: 'day',
				tooltipComponentParams: { color: '#ececec' },
			},
			// Book now button
			{
				suppressSizeToFit: true,
				flex: 1,
				headerName: "",
				cellClass: ["float-end", "text-center"],
				cellRenderer: (s) => {
					const eDiv = document.createElement('div');
					eDiv.attributes
					var d = new Date(Date.UTC.apply(null, s.data.startTime.split(',')))
					if (workshopSearchMgr.numMinutesBetween(d, new Date()) < 16) {
						eDiv.innerHTML = '<form '
							+ 'target="_blank" '
							+ 'action="/Student/BookWorkshop" '
							+ 'method="post" '
							+ 'novalidate=""novalidate '
							+ '>'
							+ '<input type="hidden" name="model.MeetingId" data-val="true" data-val-required="The meetingId field is required." id="meetingId" value="' + s.data.id + '">'
							+ '<input type="hidden" name="model.UserId" data-val="true" data-val-required="The userId field is required." id="userId" value="' + s.data.userId + '">'
							+ '<input type="hidden" name="model.Meeting" id="Meeting" value="TutorCom.BL.MeetingSpaces.MeetingProxy">'
							+ '<input type="hidden" name="model.ShouldLaunch" value="true">'
							+ '<button id="launchMeetingId_' + s.data.id + '"'
							+ 'type = "submit"'
							+ 'class="btn btn-sm btn-primary w-100" '
							+ 'title = "This button is available 15 minutes before the meeting."'
							+ 'aria-label="Launch meeting"'
							+ '>'
							+ 'Launch'


						/*
						eDiv.innerHTML = '<button id="launchMeetingId_' + s.data.id + '"'
							+ 'type = "button"'
							+ 'class="btn btn-sm btn-primary w-100" '
							+ 'title = "This button is available 15 minutes before the meeting."'
							+ 'aria-label="Launch meeting"'
							+ 'onclick="window.open(\'' + window.location.origin + '/student/BookWorkshop?meetingId=' + s.data.id + '&userId=' + s.data.userId + '&ShouldLaunch=true\')"'
							+ '>'
							
							+ 'Launch'
							*/
					}

					else {

						var dataUrl = window.location.origin + "/student/BookWorkshop?meetingId=" + s.data.id + "&userId=" + s.data.userId;
						eDiv.innerHTML = '<a class="btn btn-sm btn-secondary w-100"'
							+ 'data-bs-toggle="modal"'
							+ 'data-bs-target="#book-meeting-modal-dynamic"'
							+ 'data-url="' + dataUrl + '"'
							+ 'data-id="BookWorkshop"'
							+ 'data-meeting-id="' + s.data.id + '"'
							+ 'data-user-id="' + s.data.userId + '"' // [MR] shouldnt matter anymore
							+ 'data-title="You are about to book a workshop"'
							+ 'data-meeting-date="' + s.data.startTime + '"'
							+ '> Book Now</a > ';
					}
					return eDiv;
				}
			}
		];

		// let the grid know which columns and what data to use

		this.gridOptions = {
			defaultColDef: {
				sortable: true,
				resizable: true,
				comparator: agComparators.byLowercase,
				tooltipComponent: 'customTooltip',
			},
			columnDefs: columnDefs,
			enableCellTextSelection: true,
			rowData: null,
			//pagination: true,


			tooltipShowDelay: 300,
			components: {
				customTooltip: CustomTooltip,
			},

		};


		// Format the datetime when the data changes
		this.gridOptions.onRowDataChanged = () => {
			$.leo.global.processSpanUTCTimes();
		}
		// Maybe do the the book now modal for now nothing
		this.gridOptions.onRowDoubleClicked = (sender) => {
			this.userSelected(sender.data.userId);
		}

		var eGridDiv = document.querySelector('#grdSearchResults');

		if (eGridDiv != undefined) {
			new agGrid.Grid(eGridDiv, this.gridOptions);
		}

		$('#book-meeting-modal-dynamic').off('click').on('click', '.btn-ok', function (e) {
			if ($(".contact-form").valid()) {
				$(".contact-form").submit();
			}
		});
	},


	search: function (increment) {
		if (!this.validateForm())
			return false;
		var page = +$("#pageNumber").val();
		if (increment)
			page++;

		var url = window.location.origin
			+ '/student/WorkshopTimeslots'
			+ ($('#workshopSearchSelect').val() ? '?keywordId=' + $('#workshopSearchSelect').val() : '')
			+ (page ?
				$("#workshopSearchSelect").val() ? "&Page=" + page
					: '?Page=' + page : '')

		$.ajax({
			url: url,
			type: "GET",
			dataType: "json",
			success: (data) => {
				if (this.gridOptions === undefined) {
					workshopSearchMgr.configureResultsGrid()
				}

				if (this.gridOptions.api != undefined) {
					this.gridOptions.api.setRowData(data);
					this.gridOptions.api.sizeColumnsToFit();
				}

				$("#pageNumber").val(page)
			},
			error: function (error) {
				console.log(`Error ${error}`);
			}
		});

	},
	onGridReady: function (params) {
		params.api.sizeColumnsToFit();

		window.addEventListener('resize', function () {
			setTimeout(function () {
				params.api.sizeColumnsToFit();
			});
		});
	},


	validateForm: function () {
		return (
			true
			//&
			//$.cst.global.validation.dataListSelectionIsValid('ddlReferrer', this.providerReferrers)
		);
	},


	resetForm: function () {
		$('#txtSearchTerms').focus();
	},


	createUser: function () {
		window.location = `${window.location.origin}/Users/Edit/`;
	},


	userSelected: function (userId) {
		//window.location = `${window.location.origin}/Users/Info/UserInfo/${userId}`;
		//window.location = `${window.location.origin}/UserEdit/${userId}`;
	},

	numMinutesBetween: function (d1, d2) {
		var diff = Math.abs(d1.getTime() - d2.getTime());
		return diff / (1000 * 60);
	}


}

