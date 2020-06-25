# Makefile for Sphinx documentation
#

# You can set these variables from the command line.
SPHINXOPTS    =
SPHINXBUILD   = sphinx-build
PAPER         =
BUILD         = ../../ThinkSharply


# Internal variables.
PAPEROPT_a4     = -D latex_paper_size=a4
PAPEROPT_letter = -D latex_paper_size=letter
ALLSPHINXOPTS   = -d $(BUILD)/doctrees $(PAPEROPT_$(PAPER)) $(SPHINXOPTS) -t $(BUILD) .

.PHONY: help clean html dirhtml pickle json htmlhelp qthelp latex changes linkcheck doctest

help:
	@echo "Please use \`make <target>' where <target> is one of"
	@echo "  html      to make standalone HTML files"
	@echo "  dirhtml   to make HTML files named index.html in directories"
	@echo "  pickle    to make pickle files"
	@echo "  json      to make JSON files"
	@echo "  epub      to make epub files"
	@echo "  htmlhelp  to make HTML files and a HTML help project"
	@echo "  qthelp    to make HTML files and a qthelp project"
	@echo "  latex     to make LaTeX files, you can set PAPER=a4 or PAPER=letter"
	@echo "  changes   to make an overview of all changed/added/deprecated items"
	@echo "  linkcheck to check all external links for integrity"
	@echo "  doctest   to run all doctests embedded in the documentation (if enabled)"

clean:
	-rm -rf $(BUILD)

html:
	$(SPHINXBUILD) -b html $(ALLSPHINXOPTS) $(BUILD)
	@echo "Build finished. The HTML pages are in $(BUILD)."

dirhtml:
	$(SPHINXBUILD) -b dirhtml $(ALLSPHINXOPTS) $(BUILD)/dirhtml
	@echo
	@echo "Build finished. The HTML pages are in $(BUILD)/dirhtml."

pickle:
	$(SPHINXBUILD) -b pickle $(ALLSPHINXOPTS) $(BUILD)/pickle
	@echo "Build finished; now you can process the pickle files."

json:
	$(SPHINXBUILD) -b json $(ALLSPHINXOPTS) $(BUILD)/json
	@echo "Build finished; now you can process the JSON files."

epub:
	$(SPHINXBUILD) -b epub $(ALLSPHINXOPTS) $(BUILD)/epub
	@echo "Build finished. The output is in $(BUILD)/epub."    
    
htmlhelp:
	$(SPHINXBUILD) -b htmlhelp $(ALLSPHINXOPTS) $(BUILD)/htmlhelp
	@echo "Build finished; now you can run HTML Help Workshop with the" \
	      ".hhp project file in $(BUILD)/htmlhelp."

qthelp:
	$(SPHINXBUILD) -b qthelp $(ALLSPHINXOPTS) $(BUILD)/qthelp
	@echo "Build finished; now you can run "qcollectiongenerator" with the" \
	      ".qhcp project file in $(BUILD)/qthelp, like this:"
	@echo "# qcollectiongenerator $(BUILD)/qthelp/How to Think Like a Computer Scientist: Learning with Python.qhcp"
	@echo "To view the help file:"
	@echo "# assistant -collectionFile $(BUILD)/qthelp/How to Think Like a Computer Scientist: Learning with Python.qhc"

latex:
	$(SPHINXBUILD) -b latex $(ALLSPHINXOPTS) $(BUILD)/latex
	@echo "Build finished; the LaTeX files are in $(BUILD)/latex."
	@echo "Run \`make all-pdf' or \`make all-ps' in that directory to" \
	      "run these through (pdf)latex."

changes:
	$(SPHINXBUILD) -b changes $(ALLSPHINXOPTS) $(BUILD)/changes
	@echo "The overview file is in $(BUILD)/changes."

linkcheck:
	$(SPHINXBUILD) -b linkcheck $(ALLSPHINXOPTS) $(BUILD)/linkcheck
	@echo "Link check complete; look for any errors in the above output " \
	      "or in $(BUILD)/linkcheck/output.txt."

doctest:
	$(SPHINXBUILD) -b doctest $(ALLSPHINXOPTS) $(BUILD)/doctest
	@echo "Testing of doctests in the sources finished, look at the " \
	      "results in $(BUILD)/doctest/output.txt."
