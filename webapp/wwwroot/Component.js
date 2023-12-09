sap.ui.define([
	"sap/ui/core/UIComponent",
	"sap/ui/model/json/JSONModel",
], (UIComponent, JSONModel, Device) => {
	"use strict";

	return UIComponent.extend("ui5.anabolizantes.Component", {
		metadata: {
			interfaces: ["sap.ui.core.IAsyncContentCreation"],
			manifest: "json"
		},
		init: function () {
			UIComponent.prototype.init.apply(this, arguments);
			this.getRouter().initialize();
		},
		getContentDensityClass: function () {
			if (!this._sContentDensityClass) {
				if (!Device.support.touch) {
					this._sContentDensityClass = "sapUiSizeCompact";
				} else {
					this._sContentDensityClass = "sapUiSizeCozy";
				}
			}
			return this._sContentDensityClass;
		}
	});
});
