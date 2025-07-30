/**
 * reportEdit.js - Maneja la lógica de la vista de edición de reportes
 * @version 1.0
 * @description Controla los campos dinámicos, validaciones y eventos
 */

const ReportEditManager = {
    // Variables de estado
    indices: {
        fmIndex: 0,
        preventiveIndex: 0,
        detectionIndex: 0,
        occurrenceIndex: 0,
        detectionRootCauseIndex: 0,
        systemicRootCauseIndex: 0,
        occurrenceActionIndex: 0,
        detectionActionIndex: 0,
        verOccurrenceActionIndex: 0,
        verDetectionActionIndex: 0
    },

    // Inicialización
    init: function (initialData) {
        this.indices = {
            ...this.indices,
            ...initialData
        };

        this.setupDynamicFields();
        this.setupFilePreview();
        this.setupProcessNameSync();
        this.setupDatePickers();
    },

    // Configuración de campos dinámicos
    setupDynamicFields: function () {
        // Failure Mode
        this.setupSection({
            sectionId: 'fm',
            btnId: 'add-fm-row-btn',
            template: this.fmTemplate,
            removeClass: 'remove-fm-row-btn'
        });

        // Preventive Actions
        this.setupSection({
            sectionId: 'preventive',
            btnId: 'add-preventive-row-btn',
            template: this.preventiveTemplate,
            removeClass: 'remove-preventive-row-btn'
        });

        // Detection Controls
        this.setupSection({
            sectionId: 'detection',
            btnId: 'add-detection-row-btn',
            template: this.detectionTemplate,
            removeClass: 'remove-detection-row-btn'
        });

        // Occurrence Root Cause
        this.setupSection({
            sectionId: 'occurrence',
            btnId: 'add-occurrence-row-btn',
            template: this.occurrenceTemplate,
            removeClass: 'remove-occurrence-row-btn'
        });

        // Detection Root Cause
        this.setupSection({
            sectionId: 'detection-root-cause',
            btnId: 'add-detection-root-cause-btn',
            template: this.detectionRootCauseTemplate,
            removeClass: 'remove-detection-root-cause-btn'
        });

        // Systemic Root Cause
        this.setupSection({
            sectionId: 'systemic-root-cause',
            btnId: 'add-systemic-root-cause-btn',
            template: this.systemicRootCauseTemplate,
            removeClass: 'remove-systemic-root-cause-btn'
        });

        // Occurrence Actions
        this.setupSection({
            sectionId: 'occurrence-actions',
            btnId: 'add-occurrence-action-btn',
            template: this.occurrenceActionTemplate,
            removeClass: 'remove-occurrence-action-btn'
        });

        // Detection Actions
        this.setupSection({
            sectionId: 'detection-actions',
            btnId: 'add-detection-action-btn',
            template: this.detectionActionTemplate,
            removeClass: 'remove-detection-action-btn'
        });

        // Verification Occurrence Actions
        this.setupSection({
            sectionId: 'ver-occurrence-actions',
            btnId: 'add-ver-occurrence-action-btn',
            template: this.verOccurrenceActionTemplate,
            removeClass: 'remove-ver-occurrence-action-btn'
        });

        // Verification Detection Actions
        this.setupSection({
            sectionId: 'ver-detection-actions',
            btnId: 'add-ver-detection-action-btn',
            template: this.verDetectionActionTemplate,
            removeClass: 'remove-ver-detection-action-btn'
        });
    },

    // Configuración genérica para secciones
    setupSection: function ({ sectionId, btnId, template, removeClass }) {
        const btn = document.getElementById(btnId);
        if (!btn) return;

        btn.addEventListener('click', () => {
            const index = this.indices[`${sectionId.replace('-', '')}Index`];
            const div = document.createElement('div');
            div.className = `col-md-12 mb-3 ${sectionId}-dynamic-field-group`;
            div.dataset.index = index;
            div.innerHTML = template(index);

            const container = document.getElementById(`${sectionId}-dynamic-fields`);
            if (container) {
                container.appendChild(div);
                this.indices[`${sectionId.replace('-', '')}Index`]++;
                this.initDatePickers(div);
            }
        });

        document.addEventListener('click', (e) => {
            if (e.target.classList.contains(removeClass)) {
                const row = e.target.closest(`.${sectionId}-dynamic-field-group`);
                if (row && confirm('Are you sure you want to remove this item?')) {
                    row.remove();
                }
            }
        });
    },

    // Sincronización de nombres de proceso
    setupProcessNameSync: function () {
        document.addEventListener('change', (e) => {
            if (e.target.classList.contains('fm-process-name')) {
                const row = e.target.closest('.fm-dynamic-field-group');
                const index = row?.dataset.index;
                const processName = e.target.value;

                if (index && processName) {
                    const preventiveProcess = document.querySelector(
                        `.preventive-dynamic-field-group[data-index="${index}"] .preventive-process-name`
                    );
                    if (preventiveProcess) preventiveProcess.value = processName;

                    const detectionProcess = document.querySelector(
                        `.detection-dynamic-field-group[data-index="${index}"] .detection-process-name`
                    );
                    if (detectionProcess) detectionProcess.value = processName;
                }
            }
        });
    },

    // Vista previa de archivos
    setupFilePreview: function () {
        const fileInput = document.querySelector('input[type="file"]');
        if (!fileInput) return;

        fileInput.addEventListener('change', (e) => {
            const previewContainer = document.getElementById('preview-container');
            if (!previewContainer) return;

            previewContainer.innerHTML = '';

            if (e.target.files.length > 0) {
                const alertDiv = document.createElement('div');
                alertDiv.className = 'alert alert-warning';
                alertDiv.innerHTML = `
          <i class="fas fa-exclamation-triangle me-2"></i>
          You have selected ${e.target.files.length} new file(s) to add.
        `;
                previewContainer.appendChild(alertDiv);

                const fileList = document.createElement('div');
                fileList.className = 'list-group mt-2';

                Array.from(e.target.files).forEach((file) => {
                    const listItem = document.createElement('div');
                    listItem.className = 'list-group-item';
                    listItem.innerHTML = `
            <div class="d-flex justify-content-between align-items-center">
              <span class="text-truncate" style="max-width: 70%">${file.name}</span>
              <span class="badge bg-secondary">${(file.size / 1024 / 1024).toFixed(2)}MB</span>
            </div>
          `;
                    fileList.appendChild(listItem);
                });

                previewContainer.appendChild(fileList);
            }
        });
    },

    // Configuración de datepickers
    setupDatePickers: function () {
        this.initDatePickers(document);
    },

    initDatePickers: function (container) {
        const dateInputs = container.querySelectorAll('input[type="date"]');
        dateInputs.forEach(input => {
            // Puedes inicializar aquí algún datepicker personalizado si es necesario
            // Por ahora solo asegurarnos que los inputs de fecha estén configurados
            if (!input.value && input.name.includes('CloseDate')) {
                input.min = new Date().toISOString().split('T')[0];
            }
        });
    },

    // ==================== PLANTILLAS ====================

    // Plantilla para Failure Mode
    fmTemplate: function (index) {
        return `
      <div class="card shadow-sm mb-3">
        <div class="card-body">
          <div class="row g-3">
            <div class="col-md-3">
              <div class="form-floating">
                <input name="FmMode[${index}]" class="form-control" />
                <label>Failure Mode</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="FmProcessName[${index}]" class="form-control fm-process-name" />
                <label>Process Name</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <select name="Fm6Ms[${index}]" class="form-select">
                  <option value="">- Select -</option>
                  <option value="Manpower">Manpower</option>
                  <option value="Method">Method</option>
                  <option value="Machinary">Machinary</option>
                  <option value="Material">Material</option>
                  <option value="Measurement">Measurement</option>
                  <option value="Environment">Environment</option>
                </select>
                <label>6Ms</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="FactorUno[${index}]" class="form-control" />
                <label>Factor 1</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="FactorDos[${index}]" class="form-control" />
                <label>Factor 2</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="FactorTres[${index}]" class="form-control" />
                <label>Factor 3</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <select name="FmRelated[${index}]" class="form-select fm-related">
                  <option value="">- Select -</option>
                  <option value="Y">Yes</option>
                  <option value="N">No</option>
                </select>
                <label>Related With The Issue?</label>
              </div>
            </div>
          </div>
          <button type="button" class="btn btn-sm btn-outline-danger mt-2 remove-fm-row-btn">
            <i class="fas fa-trash me-1"></i> Remove
          </button>
        </div>
      </div>
    `;
    },

    // Plantilla para Preventive Actions
    preventiveTemplate: function (index) {
        return `
      <div class="card shadow-sm mb-3">
        <div class="card-body">
          <h6 class="text-primary">Preventive Action #${parseInt(index) + 1}</h6>
          <div class="row g-3">
            <div class="col-md-3">
              <div class="form-floating">
                <input name="PreventiveCProcessName[${index}]" class="form-control preventive-process-name" />
                <label>Process Name</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="PreventiveCManpower[${index}]" class="form-control" />
                <label>Manpower</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="PreventiveCMethod[${index}]" class="form-control" />
                <label>Method</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="PreventiveCMachinary[${index}]" class="form-control" />
                <label>Machinary</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="PreventiveCMaterial[${index}]" class="form-control" />
                <label>Material</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="PreventiveCMeasurement[${index}]" class="form-control" />
                <label>Measurement</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="PreventiveCEnvironment[${index}]" class="form-control" />
                <label>Environment</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="PreventiveCRank[${index}]" type="number" class="form-control" value="0" />
                <label>Rank [O]</label>
              </div>
            </div>
          </div>
          <button type="button" class="btn btn-sm btn-outline-danger mt-2 remove-preventive-row-btn">
            <i class="fas fa-trash me-1"></i> Remove
          </button>
        </div>
      </div>
    `;
    },

    // Plantilla para Detection Controls
    detectionTemplate: function (index) {
        return `
      <div class="card shadow-sm mb-3">
        <div class="card-body">
          <h6 class="text-warning">Detection Control #${parseInt(index) + 1}</h6>
          <div class="row g-3">
            <div class="col-md-3">
              <div class="form-floating">
                <input name="DetectionCProcessName[${index}]" class="form-control detection-process-name" />
                <label>Process Name</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="DetectionCManpower[${index}]" class="form-control" />
                <label>Manpower</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="DetectionCMethod[${index}]" class="form-control" />
                <label>Method</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="DetectionCMachinary[${index}]" class="form-control" />
                <label>Machinary</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="DetectionCMaterial[${index}]" class="form-control" />
                <label>Material</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="DetectionCMeasurement[${index}]" class="form-control" />
                <label>Measurement</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="DetectionCEnvironment[${index}]" class="form-control" />
                <label>Environment</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="DetectionCRank[${index}]" type="number" class="form-control" value="0" />
                <label>Rank [D]</label>
              </div>
            </div>
          </div>
          <button type="button" class="btn btn-sm btn-outline-danger mt-2 remove-detection-row-btn">
            <i class="fas fa-trash me-1"></i> Remove
          </button>
        </div>
      </div>
    `;
    },

    // Plantilla para Occurrence Root Cause
    occurrenceTemplate: function (index) {
        return `
      <div class="card shadow-sm mb-3">
        <div class="card-body">
          <h6 class="text-danger">Root Cause Analysis #${parseInt(index) + 1}</h6>
          <div class="row g-3">
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorUno[${index}]" class="form-control" />
                <label>Root Cause Factor</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorUnoPrimerWhy[${index}]" class="form-control" />
                <label>1st Why</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorUnoSegundoWhy[${index}]" class="form-control" />
                <label>2nd Why</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorUnoTercerWhy[${index}]" class="form-control" />
                <label>3rd Why</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorUnoCuartoWhy[${index}]" class="form-control" />
                <label>4th Why</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorUnoQuintoWhy[${index}]" class="form-control" />
                <label>5th Why</label>
              </div>
            </div>
            <div class="col-md-12">
              <div class="form-floating">
                <textarea name="FactorUnoCorrectiveActions[${index}]" class="form-control" style="height: 100px"></textarea>
                <label>Corrective Actions</label>
              </div>
            </div>
          </div>
          <button type="button" class="btn btn-sm btn-outline-danger mt-2 remove-occurrence-row-btn">
            <i class="fas fa-trash me-1"></i> Remove
          </button>
        </div>
      </div>
    `;
    },

    // Plantilla para Detection Root Cause
    detectionRootCauseTemplate: function (index) {
        return `
      <div class="card shadow-sm mb-3">
        <div class="card-body">
          <h6 class="text-secondary">Detection Root Cause Analysis #${parseInt(index) + 1}</h6>
          <div class="row g-3">
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorDos[${index}]" class="form-control" />
                <label>Root Cause Factor</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorDosPrimerWhy[${index}]" class="form-control" />
                <label>1st Why</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorDosSegundoWhy[${index}]" class="form-control" />
                <label>2nd Why</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorDosTercerWhy[${index}]" class="form-control" />
                <label>3rd Why</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorDosCuartoWhy[${index}]" class="form-control" />
                <label>4th Why</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorDosQuintoWhy[${index}]" class="form-control" />
                <label>5th Why</label>
              </div>
            </div>
            <div class="col-md-12">
              <div class="form-floating">
                <textarea name="FactorDosCorrectiveActions[${index}]" class="form-control" style="height: 100px"></textarea>
                <label>Corrective Actions</label>
              </div>
            </div>
          </div>
          <button type="button" class="btn btn-sm btn-outline-secondary mt-2 remove-detection-root-cause-btn">
            <i class="fas fa-trash me-1"></i> Remove
          </button>
        </div>
      </div>
    `;
    },

    // Plantilla para Systemic Root Cause
    systemicRootCauseTemplate: function (index) {
        return `
      <div class="card shadow-sm mb-3">
        <div class="card-body">
          <h6 class="text-dark">Systemic Root Cause Analysis #${parseInt(index) + 1}</h6>
          <div class="row g-3">
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorTres[${index}]" class="form-control" />
                <label>Root Cause Factor</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorTresPrimerWhy[${index}]" class="form-control" />
                <label>1st Why</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorTresSegundoWhy[${index}]" class="form-control" />
                <label>2nd Why</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorTresTercerWhy[${index}]" class="form-control" />
                <label>3rd Why</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorTresCuartoWhy[${index}]" class="form-control" />
                <label>4th Why</label>
              </div>
            </div>
            <div class="col-md-4">
              <div class="form-floating">
                <input name="FactorTresQuintoWhy[${index}]" class="form-control" />
                <label>5th Why</label>
              </div>
            </div>
            <div class="col-md-12">
              <div class="form-floating">
                <textarea name="FactorTresCorrectiveActions[${index}]" class="form-control" style="height: 100px"></textarea>
                <label>Corrective Actions</label>
              </div>
            </div>
          </div>
          <button type="button" class="btn btn-sm btn-outline-dark mt-2 remove-systemic-root-cause-btn">
            <i class="fas fa-trash me-1"></i> Remove
          </button>
        </div>
      </div>
    `;
    },

    // Plantilla para Occurrence Actions
    occurrenceActionTemplate: function (index) {
        return `
      <div class="card shadow-sm mb-3">
        <div class="card-body">
          <h6 class="text-success">Occurrence Action #${parseInt(index) + 1}</h6>
          <div class="row g-3">
            <div class="col-md-3">
              <div class="form-floating">
                <input name="OccurrenceItems[${index}]" class="form-control" />
                <label>Action Item</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="OccurrenceAction[${index}]" class="form-control" />
                <label>Corrective Action</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="OccurrenceResponsable[${index}]" class="form-control" />
                <label>Responsible</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="OccurrenceDepartment[${index}]" class="form-control" />
                <label>Department</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="OccurrenceOpeningDate[${index}]" type="date" class="form-control" />
                <label>Opening Date</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="OccurrenceCloseDate[${index}]" type="date" class="form-control" />
                <label>Close Date</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="OccurrenceAmef[${index}]" class="form-control" />
                <label>AMEF Update</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="OccurrenceCp[${index}]" class="form-control" />
                <label>Control Plan Update</label>
              </div>
            </div>
          </div>
          <button type="button" class="btn btn-sm btn-outline-success mt-2 remove-occurrence-action-btn">
            <i class="fas fa-trash me-1"></i> Remove
          </button>
        </div>
      </div>
    `;
    },

    // Plantilla para Detection Actions
    detectionActionTemplate: function (index) {
        return `
      <div class="card shadow-sm mb-3">
        <div class="card-body">
          <h6 class="text-info">Detection Action #${parseInt(index) + 1}</h6>
          <div class="row g-3">
            <div class="col-md-3">
              <div class="form-floating">
                <input name="DetectionItems[${index}]" class="form-control" />
                <label>Action Item</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="DetectionAction[${index}]" class="form-control" />
                <label>Corrective Action</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="DetectionResponsable[${index}]" class="form-control" />
                <label>Responsible</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="DetectionDepartment[${index}]" class="form-control" />
                <label>Department</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="DetectionOpeningDate[${index}]" type="date" class="form-control" />
                <label>Opening Date</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="DetectionCloseDate[${index}]" type="date" class="form-control" />
                <label>Close Date</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="DetectionAmef[${index}]" class="form-control" />
                <label>AMEF Update</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="DetectionCp[${index}]" class="form-control" />
                <label>Control Plan Update</label>
              </div>
            </div>
          </div>
          <button type="button" class="btn btn-sm btn-outline-info mt-2 remove-detection-action-btn">
            <i class="fas fa-trash me-1"></i> Remove
          </button>
        </div>
      </div>
    `;
    },

    // Plantilla para Verification Occurrence Actions
    verOccurrenceActionTemplate: function (index) {
        return `
      <div class="card shadow-sm mb-3">
        <div class="card-body">
          <h6 class="text-primary">Verification Occurrence Action #${parseInt(index) + 1}</h6>
          <div class="row g-3">
            <div class="col-md-3">
              <div class="form-floating">
                <input name="VerOccurrenceItems[${index}]" class="form-control" />
                <label>Action Item</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="VerOccurrenceAction[${index}]" class="form-control" />
                <label>Corrective Action</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="VerOccurrenceResponsable[${index}]" class="form-control" />
                <label>Responsible</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="VerOccurrenceDepartment[${index}]" class="form-control" />
                <label>Department</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="VerOccurrenceOpeningDate[${index}]" type="date" class="form-control" />
                <label>Opening Date</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="VerOccurrenceCloseDate[${index}]" type="date" class="form-control" />
                <label>Close Date</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="VerOccurrenceAmef[${index}]" class="form-control" />
                <label>AMEF Update</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="VerOccurrenceCp[${index}]" class="form-control" />
                <label>Control Plan Update</label>
              </div>
            </div>
          </div>
          <button type="button" class="btn btn-sm btn-outline-primary mt-2 remove-ver-occurrence-action-btn">
            <i class="fas fa-trash me-1"></i> Remove
          </button>
        </div>
      </div>
    `;
    },

    // Plantilla para Verification Detection Actions
    verDetectionActionTemplate: function (index) {
        return `
      <div class="card shadow-sm mb-3">
        <div class="card-body">
          <h6 class="text-info">Verification Detection Action #${parseInt(index) + 1}</h6>
          <div class="row g-3">
            <div class="col-md-3">
              <div class="form-floating">
                <input name="VerDetectionItems[${index}]" class="form-control" />
                <label>Action Item</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="VerDetectionAction[${index}]" class="form-control" />
                <label>Corrective Action</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="VerDetectionResponsable[${index}]" class="form-control" />
                <label>Responsible</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="VerDetectionDepartment[${index}]" class="form-control" />
                <label>Department</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="VerDetectionOpeningDate[${index}]" type="date" class="form-control" />
                <label>Opening Date</label>
              </div>
            </div>
            <div class="col-md-2">
              <div class="form-floating">
                <input name="VerDetectionCloseDate[${index}]" type="date" class="form-control" />
                <label>Close Date</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="VerDetectionAmef[${index}]" class="form-control" />
                <label>AMEF Update</label>
              </div>
            </div>
            <div class="col-md-3">
              <div class="form-floating">
                <input name="VerDetectionCp[${index}]" class="form-control" />
                <label>Control Plan Update</label>
              </div>
            </div>
          </div>
          <button type="button" class="btn btn-sm btn-outline-info mt-2 remove-ver-detection-action-btn">
            <i class="fas fa-trash me-1"></i> Remove
          </button>
        </div>
      </div>
    `;
    }
};

// Inicialización cuando el DOM esté listo
if (document.readyState === 'complete' || document.readyState === 'interactive') {
    setTimeout(() => {
        if (window.reportEditInitialData) {
            ReportEditManager.init(window.reportEditInitialData);
        }
    }, 1);
} else {
    document.addEventListener('DOMContentLoaded', function () {
        if (window.reportEditInitialData) {
            ReportEditManager.init(window.reportEditInitialData);
        }
    });
}