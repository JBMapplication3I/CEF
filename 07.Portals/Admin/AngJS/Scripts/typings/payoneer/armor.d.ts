/**
 * Payoneer Escrow modal handler
 *
 * This object handles everything related to authenticated modals and in-app
 * modals.
 */
interface IArmor {

    ///////////////////////////////////////////////////////////////////////
    // PROPERTIES ////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////

    /**
     * @property {object} ap_modal The jQuery ap-modal element.
     */
    ap_modal: any,

    /**
     * @property {string} modal_id The ID selector of the topmost container.
     */
    modal_id: string,

    /**
     * @property {boolean} reload_parent_page The flag that tells the page to reload on modal close.
     */
    reload_parent_page: boolean,

    /**
     * @property {function} callback is a function intended to be triggered on modal close.
     */
    callback: (...params: any[]) => any | void,

    /**
     * @property {string} base_url The base URL for modal.css to use.
     */
    base_url: string,

    ///////////////////////////////////////////////////////////////////////
    // INITIALIZATION ////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////

    /**
     * Initialize the modal and load it's styles.
     */
    init: () => void,

    ///////////////////////////////////////////////////////////////////////
    // FUNCTIONS /////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////

    /**
     * Add a parameter to the URL.
     *
     * @param {string} url   The URL to add the parameter to
     * @param {string} key   The key of the parameter
     * @param {string} value The value of the parameter
     *
     * @returns {string} Returns the full parameter
     */
    addUrlParam: (url: string, key: string, value: string) => string,

    /**
     * Disable the script if the base URL was not found. The base URL is
     * required for modal.css to load in the DOM. Without modal.css, modals
     * will not render properly.
     */
    disableScript: () => void,

    /**
     * Get the base URL of a file.
     *
     * @param {string} pattern The pattern to use to get the base URL.
     *
     * @return {string|null}
     */
    getBaseUrl: (filename: string) => string,

    /**
     * Load stylesheets and append to the head tag.
     */
    loadStyle: () => void,

    /**
     * Open a modal.
     * 
     * @param {string}   url       The URL that gets put into the src attribute of the iframe.
     * @param {bool}     reload    (optional) Reload the page? (This strips out the action query parameter)
     * @param {string}   modalId   (optional) The ID selector to use for the modal if provided. This defaults to 'ap-modal'.
     * @param {function} callback  (optional) A callback function to add to the listener.
     *
     * This function builds the following HTML:
     *
     * <div class="ap-modal has-iframe" id="{modalId}">
     *   <div class="ap-modal-content">
     *     <div class="ap-modal-iframe">
     *       <iframe src="{url}" allowtransparency="true" id="{modalId}_frame" name="{modalId}_frame"></iframe>
     *     </div>
     *   </div>
     * </div>
     */
    openModal: (url: string, reload: boolean, modalId: string, callback: (...parms: any[]) => any|void) => void,

    /**
     * Close the modal.
     */
    closeModal: (reload: boolean) => void,

    /**
     * Reload the parent page.
     */
    reloadParentPage: () => void,

    /**
     * Run an event listener on a parent window. This listener includes default
     * events used for the Armor Payments application, but can be provided a
     * callback to be executed.
     */
    runEventListener: () => void,

    /**
     * Trigger the callback (if one was provided when openModal() was called)
     */
    triggerCallback: () => void,
}

declare var armor: IArmor;
