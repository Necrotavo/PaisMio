'use strict';


customElements.define('compodoc-menu', class extends HTMLElement {
    constructor() {
        super();
        this.isNormalMode = this.getAttribute('mode') === 'normal';
    }

    connectedCallback() {
        this.render(this.isNormalMode);
    }

    render(isNormalMode) {
        let tp = lithtml.html(`
        <nav>
            <ul class="list">
                <li class="title">
                    <a href="index.html" data-type="index-link">pais-mio-app documentation</a>
                </li>

                <li class="divider"></li>
                ${ isNormalMode ? `<div id="book-search-input" role="search"><input type="text" placeholder="Type to search"></div>` : '' }
                <li class="chapter">
                    <a data-type="chapter-link" href="index.html"><span class="icon ion-ios-home"></span>Getting started</a>
                    <ul class="links">
                        <li class="link">
                            <a href="overview.html" data-type="chapter-link">
                                <span class="icon ion-ios-keypad"></span>Overview
                            </a>
                        </li>
                        <li class="link">
                            <a href="index.html" data-type="chapter-link">
                                <span class="icon ion-ios-paper"></span>README
                            </a>
                        </li>
                                <li class="link">
                                    <a href="dependencies.html" data-type="chapter-link">
                                        <span class="icon ion-ios-list"></span>Dependencies
                                    </a>
                                </li>
                    </ul>
                </li>
                    <li class="chapter modules">
                        <a data-type="chapter-link" href="modules.html">
                            <div class="menu-toggler linked" data-toggle="collapse" ${ isNormalMode ?
                                'data-target="#modules-links"' : 'data-target="#xs-modules-links"' }>
                                <span class="icon ion-ios-archive"></span>
                                <span class="link-name">Modules</span>
                                <span class="icon ion-ios-arrow-down"></span>
                            </div>
                        </a>
                        <ul class="links collapse " ${ isNormalMode ? 'id="modules-links"' : 'id="xs-modules-links"' }>
                            <li class="link">
                                <a href="modules/AppModule.html" data-type="entity-link">AppModule</a>
                                    <li class="chapter inner">
                                        <div class="simple menu-toggler" data-toggle="collapse" ${ isNormalMode ?
                                            'data-target="#components-links-module-AppModule-ce976a6802ad5ea26b5e7d69243f177e"' : 'data-target="#xs-components-links-module-AppModule-ce976a6802ad5ea26b5e7d69243f177e"' }>
                                            <span class="icon ion-md-cog"></span>
                                            <span>Components</span>
                                            <span class="icon ion-ios-arrow-down"></span>
                                        </div>
                                        <ul class="links collapse" ${ isNormalMode ? 'id="components-links-module-AppModule-ce976a6802ad5ea26b5e7d69243f177e"' :
                                            'id="xs-components-links-module-AppModule-ce976a6802ad5ea26b5e7d69243f177e"' }>
                                            <li class="link">
                                                <a href="components/AdminViewComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">AdminViewComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/AppComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">AppComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/ChangePasswordComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">ChangePasswordComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/ErrorPageComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">ErrorPageComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/IndexPmAppComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">IndexPmAppComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/InventoryControlComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">InventoryControlComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/NavbarComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">NavbarComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/OrderViewComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">OrderViewComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/RecoverPasswordComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">RecoverPasswordComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/ReportViewComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">ReportViewComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/SignInComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">SignInComponent</a>
                                            </li>
                                            <li class="link">
                                                <a href="components/UserViewComponent.html"
                                                    data-type="entity-link" data-context="sub-entity" data-context-id="modules">UserViewComponent</a>
                                            </li>
                                        </ul>
                                    </li>
                                <li class="chapter inner">
                                    <div class="simple menu-toggler" data-toggle="collapse" ${ isNormalMode ?
                                        'data-target="#injectables-links-module-AppModule-ce976a6802ad5ea26b5e7d69243f177e"' : 'data-target="#xs-injectables-links-module-AppModule-ce976a6802ad5ea26b5e7d69243f177e"' }>
                                        <span class="icon ion-md-arrow-round-down"></span>
                                        <span>Injectables</span>
                                        <span class="icon ion-ios-arrow-down"></span>
                                    </div>
                                    <ul class="links collapse" ${ isNormalMode ? 'id="injectables-links-module-AppModule-ce976a6802ad5ea26b5e7d69243f177e"' :
                                        'id="xs-injectables-links-module-AppModule-ce976a6802ad5ea26b5e7d69243f177e"' }>
                                        <li class="link">
                                            <a href="injectables/ApiService.html"
                                                data-type="entity-link" data-context="sub-entity" data-context-id="modules" }>ApiService</a>
                                        </li>
                                        <li class="link">
                                            <a href="injectables/AuthService.html"
                                                data-type="entity-link" data-context="sub-entity" data-context-id="modules" }>AuthService</a>
                                        </li>
                                    </ul>
                                </li>
                            </li>
                            <li class="link">
                                <a href="modules/AppRoutingModule.html" data-type="entity-link">AppRoutingModule</a>
                            </li>
                </ul>
                </li>
                    <li class="chapter">
                        <div class="simple menu-toggler" data-toggle="collapse" ${ isNormalMode ? 'data-target="#classes-links"' :
                            'data-target="#xs-classes-links"' }>
                            <span class="icon ion-ios-paper"></span>
                            <span>Classes</span>
                            <span class="icon ion-ios-arrow-down"></span>
                        </div>
                        <ul class="links collapse " ${ isNormalMode ? 'id="classes-links"' : 'id="xs-classes-links"' }>
                            <li class="link">
                                <a href="classes/Analysis.html" data-type="entity-link">Analysis</a>
                            </li>
                            <li class="link">
                                <a href="classes/AnalysisPC.html" data-type="entity-link">AnalysisPC</a>
                            </li>
                            <li class="link">
                                <a href="classes/AppPage.html" data-type="entity-link">AppPage</a>
                            </li>
                            <li class="link">
                                <a href="classes/Cellar.html" data-type="entity-link">Cellar</a>
                            </li>
                            <li class="link">
                                <a href="classes/CellarAdmin.html" data-type="entity-link">CellarAdmin</a>
                            </li>
                            <li class="link">
                                <a href="classes/Client.html" data-type="entity-link">Client</a>
                            </li>
                            <li class="link">
                                <a href="classes/EntryInput.html" data-type="entity-link">EntryInput</a>
                            </li>
                            <li class="link">
                                <a href="classes/InfoPaisMio.html" data-type="entity-link">InfoPaisMio</a>
                            </li>
                            <li class="link">
                                <a href="classes/Input.html" data-type="entity-link">Input</a>
                            </li>
                            <li class="link">
                                <a href="classes/InputComparativeReport.html" data-type="entity-link">InputComparativeReport</a>
                            </li>
                            <li class="link">
                                <a href="classes/InputCompared.html" data-type="entity-link">InputCompared</a>
                            </li>
                            <li class="link">
                                <a href="classes/InputEntryReport.html" data-type="entity-link">InputEntryReport</a>
                            </li>
                            <li class="link">
                                <a href="classes/InputEntryReported.html" data-type="entity-link">InputEntryReported</a>
                            </li>
                            <li class="link">
                                <a href="classes/InputQ.html" data-type="entity-link">InputQ</a>
                            </li>
                            <li class="link">
                                <a href="classes/InputReport.html" data-type="entity-link">InputReport</a>
                            </li>
                            <li class="link">
                                <a href="classes/InputRequest.html" data-type="entity-link">InputRequest</a>
                            </li>
                            <li class="link">
                                <a href="classes/InputRequestDesicion.html" data-type="entity-link">InputRequestDesicion</a>
                            </li>
                            <li class="link">
                                <a href="classes/LoginUser.html" data-type="entity-link">LoginUser</a>
                            </li>
                            <li class="link">
                                <a href="classes/MoveInput.html" data-type="entity-link">MoveInput</a>
                            </li>
                            <li class="link">
                                <a href="classes/Order.html" data-type="entity-link">Order</a>
                            </li>
                            <li class="link">
                                <a href="classes/OrderReport.html" data-type="entity-link">OrderReport</a>
                            </li>
                            <li class="link">
                                <a href="classes/Product.html" data-type="entity-link">Product</a>
                            </li>
                            <li class="link">
                                <a href="classes/ProductInOrder.html" data-type="entity-link">ProductInOrder</a>
                            </li>
                            <li class="link">
                                <a href="classes/ReportedInput.html" data-type="entity-link">ReportedInput</a>
                            </li>
                            <li class="link">
                                <a href="classes/Unit.html" data-type="entity-link">Unit</a>
                            </li>
                            <li class="link">
                                <a href="classes/User.html" data-type="entity-link">User</a>
                            </li>
                            <li class="link">
                                <a href="classes/UserChangePass.html" data-type="entity-link">UserChangePass</a>
                            </li>
                            <li class="link">
                                <a href="classes/UserRolUpgrade.html" data-type="entity-link">UserRolUpgrade</a>
                            </li>
                        </ul>
                    </li>
                        <li class="chapter">
                            <div class="simple menu-toggler" data-toggle="collapse" ${ isNormalMode ? 'data-target="#injectables-links"' :
                                'data-target="#xs-injectables-links"' }>
                                <span class="icon ion-md-arrow-round-down"></span>
                                <span>Injectables</span>
                                <span class="icon ion-ios-arrow-down"></span>
                            </div>
                            <ul class="links collapse " ${ isNormalMode ? 'id="injectables-links"' : 'id="xs-injectables-links"' }>
                                <li class="link">
                                    <a href="injectables/ApiService.html" data-type="entity-link">ApiService</a>
                                </li>
                                <li class="link">
                                    <a href="injectables/AuthService.html" data-type="entity-link">AuthService</a>
                                </li>
                                <li class="link">
                                    <a href="injectables/DataService.html" data-type="entity-link">DataService</a>
                                </li>
                            </ul>
                        </li>
                    <li class="chapter">
                        <div class="simple menu-toggler" data-toggle="collapse" ${ isNormalMode ? 'data-target="#guards-links"' :
                            'data-target="#xs-guards-links"' }>
                            <span class="icon ion-ios-lock"></span>
                            <span>Guards</span>
                            <span class="icon ion-ios-arrow-down"></span>
                        </div>
                        <ul class="links collapse " ${ isNormalMode ? 'id="guards-links"' : 'id="xs-guards-links"' }>
                            <li class="link">
                                <a href="guards/TierOneGuard.html" data-type="entity-link">TierOneGuard</a>
                            </li>
                            <li class="link">
                                <a href="guards/TierThreeGuard.html" data-type="entity-link">TierThreeGuard</a>
                            </li>
                            <li class="link">
                                <a href="guards/TierTwoGuard.html" data-type="entity-link">TierTwoGuard</a>
                            </li>
                        </ul>
                    </li>
                    <li class="chapter">
                        <div class="simple menu-toggler" data-toggle="collapse" ${ isNormalMode ? 'data-target="#miscellaneous-links"'
                            : 'data-target="#xs-miscellaneous-links"' }>
                            <span class="icon ion-ios-cube"></span>
                            <span>Miscellaneous</span>
                            <span class="icon ion-ios-arrow-down"></span>
                        </div>
                        <ul class="links collapse " ${ isNormalMode ? 'id="miscellaneous-links"' : 'id="xs-miscellaneous-links"' }>
                            <li class="link">
                                <a href="miscellaneous/variables.html" data-type="entity-link">Variables</a>
                            </li>
                        </ul>
                    </li>
                        <li class="chapter">
                            <a data-type="chapter-link" href="routes.html"><span class="icon ion-ios-git-branch"></span>Routes</a>
                        </li>
                    <li class="chapter">
                        <a data-type="chapter-link" href="coverage.html"><span class="icon ion-ios-stats"></span>Documentation coverage</a>
                    </li>
                    <li class="divider"></li>
                    <li class="copyright">
                        Documentation generated using <a href="https://compodoc.app/" target="_blank">
                            <img data-src="images/compodoc-vectorise.png" class="img-responsive" data-type="compodoc-logo">
                        </a>
                    </li>
            </ul>
        </nav>
        `);
        this.innerHTML = tp.strings;
    }
});